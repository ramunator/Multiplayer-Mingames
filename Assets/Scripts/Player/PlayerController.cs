using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class PlayerController : MonoBehaviour 
{

    public InputMaster controls;

    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cam;
    [SerializeField] private float speed;
    [SerializeField] private float turnSmoothTime = .1f;
    [SerializeField] private SkinnedMeshRenderer playerMeshRenderer;
    [SerializeField] private List<Material> playerMaterials = new List<Material>();
    [SerializeField] private int currentMatIndex = -1;
    public GameObject hand;
    [SerializeField] private LayerMask pickupLayer;
    public IPickup Inventory;
    public Transform inventoryObject;

    float turnSmoothVelocity;
    
    public float jumpVel;

    private Rigidbody rb;
    private Animator anim;
    private Vector3 moveDirection;
    private Vector3 moveDir;
    float closestCustomer = 2;
    bool hasObjectInHand;

    //GroundCheck
    public float gravity = -9.81f;
    public LayerMask ground;
    public Transform groundCheck;
    private Vector3 vel;
    private bool isGrounded;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 

        controls = new InputMaster();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        controls.Player.Move.started += ctx => Move(ctx.ReadValue<Vector2>());
        controls.Player.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        controls.Player.Move.canceled += ctx => moveDirection = Vector3.zero;

        controls.Player.Jump.performed += ctx => Jump();

        controls.Player.Pickup.performed += ctx => CheckPickup();

        controls.Player.Drop.performed += ctx => Drop();
        
        controls.Player.Serve.performed += ctx => ServeFood();

        Application.targetFrameRate = 60;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void RemoveObjectInHand()
    {
        Inventory = null;
        inventoryObject = null;
        hasObjectInHand = false;
    }

    private void Jump()
    {
        if (!isGrounded) { return; }
        Debug.Log("Should Jump Now!");
        vel.y = Mathf.Sqrt(jumpVel * -2.0f * gravity);
    }

    private void Move(Vector2 direction)
    {
        if(GameManager.Instance != null)
        {
            if (GameManager.Instance.buildMode)
            {
                moveDirection = Vector3.zero;
                return;
            }
        }


        moveDirection = new Vector3(direction.x, 0, direction.y);

        
    }
    
    void ChangePlayerMaterial()
    {
      
        currentMatIndex += 1;
        if (currentMatIndex >= playerMaterials.Count - 1) { 
            currentMatIndex = -1;
        }
        playerMeshRenderer.material = playerMaterials[currentMatIndex + 1];
    }

    private void CheckPickup()
    {
        Collider[] colliders = Physics.OverlapSphere(hand.transform.position, .8f, pickupLayer);    

        if(colliders.Length <= 0 || Inventory != null) { return; }

        if (colliders[0].TryGetComponent<IPickup>(out IPickup _pickup))
        {
            _pickup.Pickup(_pickup.pickup);
            inventoryObject = _pickup.pickup.transform;
            inventoryObject.parent = hand.transform;
            hasObjectInHand = true;

            //Make Pickup text "Press "E" To Pickup {Food}
        }
    }

    private void Drop()
    {
        if(Inventory == null) { return; }


        Inventory.Drop();
        hasObjectInHand = false;

        inventoryObject.transform.parent = null;


        inventoryObject.transform.position += new Vector3(0, 1.2f, 0);
        inventoryObject.GetComponent<Rigidbody>().velocity += new Vector3(moveDir.x * 8.5f, inventoryObject.position.y, moveDir.z * 8.5f);
        inventoryObject.GetComponent<BoxCollider>().enabled = true;
        inventoryObject = null;
    }

    private void ServeFood()
    {
        if (Inventory == null) { return; }



        IServeable _food = inventoryObject.GetComponent<IServeable>();

        GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer").ToArray();
        GameObject closestCustomerGameobject = null;
        foreach(GameObject _customer in customers)
        {
            if(Vector3.Distance(_customer.transform.position, transform.position) < closestCustomer)
            {
                closestCustomer = Vector3.Distance(_customer.transform.position, transform.position);
                closestCustomerGameobject = _customer;
                Debug.Log("Test");
            }
        }



        if(closestCustomerGameobject == null) { return; }

        Order customerOrder = closestCustomerGameobject.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Order>();

        if (customerOrder.currentOrder != inventoryObject.GetComponent<Food>().reciepe) 
        {
            GetComponent<SFXPlayer>().PlaySFX(0);

            return;
        }

        Order order = closestCustomerGameobject.transform.GetChild(1).GetChild(0).Find("Order").GetComponent<Order>();

        order.hasGotFood = true;
        order.isSitting = false;



        order.customerObject.anim.SetBool("IsWalking", true);
        order.customerObject.rb.isKinematic = false;

        _food.Serve();

        WhiteBoard.RemoveOrder(order);

        closestCustomerGameobject.GetComponent<CustomerObject>().anim.SetBool("HasGotOrder", true);
        closestCustomerGameobject.GetComponent<CustomerObject>().anim.SetBool("IsWalking", true);
        GameObject tray = closestCustomerGameobject.transform.Find("Tray").gameObject;
        tray.SetActive(true);

        tray.GetComponent<Tray>().mainItemPlaced = inventoryObject.gameObject;
        GetComponent<SFXPlayer>().PlaySFX(1);
        GameManager.AddCash(10);

        inventoryObject.parent = tray.transform.GetChild(1);
        inventoryObject.GetComponent<Rigidbody>().isKinematic = true;

        inventoryObject = null;
    }

    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Keyboard.current.tabKey.isPressed)
        {
            ChangePlayerMaterial();
        }

        if (moveDirection.magnitude >= 0.1f)
        {
            cam = Camera.main.transform;
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            anim.SetBool("Running", true);
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("Running", false);
            moveDirection = Vector3.zero;
        }



    }
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, .4f, ground);

        if (isGrounded && vel.y < 0)
        {
            vel.y = -2f;
        }

        vel.y += gravity * Time.deltaTime;

        controller.Move(vel * Time.deltaTime);

        if (inventoryObject != null && hasObjectInHand)
        {
            inventoryObject.transform.position = hand.transform.position;
            inventoryObject.GetComponent<BoxCollider>().enabled = false;

        }
    }
}

