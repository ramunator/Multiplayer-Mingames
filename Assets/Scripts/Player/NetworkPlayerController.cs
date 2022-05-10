using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Linq;
using Mirror;
using Cinemachine;
using Steamworks;
using StarterAssets;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.Utilities;
using EZCameraShake;

public class NetworkPlayerController : NetworkBehaviour 
{
    public Holdable currentHoldableItem;

    public InputMaster controls;


    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cam;
    [SerializeField] private float speed;
    [SerializeField] private float turnSmoothTime = .1f;
    [SerializeField] private SkinnedMeshRenderer playerMeshRenderer;
    [SerializeField] private List<Material> playerMaterials = new List<Material>();
    [SerializeField] private TMPro.TMP_Text playerNameText;
    [SerializeField] private ParticleSystem bloodParticle;
    [SyncVar(hook =nameof(HandlePlayerIndexChanged))] [SerializeField] private int currentMatIndex = -1;
    public GameObject hand;
    [SerializeField] private LayerMask pickupLayer;
    public IPickup Inventory;
    public Transform inventoryObject;

    float turnSmoothVelocity;
    float walkSFXDelay;
    
    public float jumpVel;

    private Rigidbody rb;
    private Animator anim;
    private Vector3 moveDirection;
    private Vector3 moveDir;
    float closestCustomer = 2;
    bool hasObjectInHand;

    [Space(15)]

    public float CameraAngleOverride = 0.0f;

    public GameObject CinemachineCameraTarget;

    public float TopClamp = 70.0f;

    public float BottomClamp = -30.0f;

    private StarterAssetsInputs _input;

    private const float _threshold = 0.01f;

    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    //GroundCheck

    [Space(15)]
    public float gravity = -9.81f;
    public LayerMask ground;
    public Transform groundCheck;
    private Vector3 vel;
    private bool isGrounded;

    public EXboxOrigin XBoxOrigin;
    public InputHandle_t handles;
    public InputDigitalActionHandle_t action;

    public InputUser input_user_;
    // Start is called before the first frame update
    void Awake()
    {
        

        controls = new InputMaster();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        _input = GetComponent<StarterAssetsInputs>();



        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        if (SteamManager.Initialized)
        {
            playerNameText.text = SteamFriends.GetPersonaName();

            
        }
    }

    public void CheckForEnablePlayerComp()
    {
        if (hasAuthority && SceneManager.GetActiveScene().name.StartsWith("Minimap_"))
        {
            transform.Find("CM FreeLook1").GetComponent<CinemachineVirtualCamera>().enabled = true;
            playerNameText.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            controls.Player.Move.started += ctx => Move(ctx.ReadValue<Vector2>());
            controls.Player.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
            controls.Player.Move.canceled += ctx => moveDirection = Vector3.zero;

            controls.Player.Jump.performed += ctx => CmdJump();

            controls.Player.Shoot.performed += ctx => CmdShoot();
        }
    }

    public void Test(EXboxOrigin origin)
    {
        //Debug.Log(SteamInput.GetDigitalActionData(handles, action).bState);
        InputActionSetHandle_t setHandle = SteamInput.GetActionSetHandle("Move");
        //Debug.Log(setHandle);
    }

    public override void OnStartAuthority()
    {
        enabled = true;



        CheckForEnablePlayerComp();
    }
    
    private void LateUpdate()
    {
        CameraRotation();


        Test(XBoxOrigin);
    }

    [ClientCallback]
    private void OnEnable()
    {
        controls.Enable();
    }

    [ClientCallback]
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

    [Command()]
    private void CmdJump()
    {
        if (!isGrounded || !hasAuthority) { return; }

        anim.SetBool("Jump", true);

        AudioManager.instance.Play("PlayerJump");

        vel.y = Mathf.Sqrt(jumpVel * -2.0f * gravity);
    }

    [Command()]
    private void CmdShoot()
    {
        if(currentHoldableItem.type != Holdable.Type.Gun) { return; }

        if(!GetComponent<playerObjectController>().gun.TryGetComponent<Gun>(out Gun gun)) { return; }

        if(gun != null)
        {
            StartCoroutine(gun.Shoot());
        }
    }

    private void CameraRotation()
    {

        // if there is an input and camera position is not fixed
        if (_input.look.sqrMagnitude >= _threshold)
        {
            _cinemachineTargetYaw += _input.look.x * Time.deltaTime;
            _cinemachineTargetPitch += _input.look.y * Time.deltaTime;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
    }
 
    public IEnumerator Die()
    {
        AudioManager.instance.Play("PlayerDie");
        var bloodParticleInstance = Instantiate(bloodParticle);
        bloodParticleInstance.transform.position = transform.Find("Root Transform").position;
        NetworkServer.Destroy(this.gameObject);
        Destroy(this.gameObject);

        CameraShaker.Instance.ShakeOnce(.4f, .4f, .1f, .75f);

        yield return new WaitForSeconds(.25f);
        Destroy(bloodParticleInstance);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    [Client]
    private void Move(Vector2 direction)
    {
        if (!isLocalPlayer) { return; }
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

    private void HandlePlayerIndexChanged(int oldIndex, int newIndex)
    {
        Debug.Log(newIndex + " | " + (playerMaterials.Count - 1));
        if (newIndex >= playerMaterials.Count - 1)
        {
            newIndex = 0;
        }
        playerMeshRenderer.material = playerMaterials[newIndex];
    }

    private void CheckPickup()
    {
        Collider[] colliders = Physics.OverlapSphere(hand.transform.position, .95f, pickupLayer);    

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

    

    [ClientCallback]
    void FixedUpdate()
    {
        if (moveDirection.magnitude >= 0.1f)
        {
            cam = Camera.main.transform;
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            anim.SetBool("Running", true);
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            if (walkSFXDelay > Random.Range(.2f, .4f) && isGrounded)
            {
                AudioManager.instance.Play("PlayerWalk");
                walkSFXDelay = 0;
            }
        }
        else
        {
            anim.SetBool("Running", false);
            moveDirection = Vector3.zero;
        }



    }


    [Command]
    void CmdSyncPosRot(Vector3 localPosition, Quaternion localRotation)
    {
        RpcSyncPosRot(localPosition, localRotation);
    }

    [ClientRpc]
    void RpcSyncPosRot(Vector3 localPosition, Quaternion localRotation)
    {
        if (!isLocalPlayer)
        {
            transform.localPosition = localPosition;
            transform.localRotation = localRotation;
        }
    }
    [ClientCallback]
    private void Update()
    {
        walkSFXDelay += Time.deltaTime;

        if (hasAuthority && SceneManager.GetActiveScene().name.StartsWith("Minimap_"))
        {
            if(playerMeshRenderer.transform.gameObject.activeSelf == false)
            {
                playerMeshRenderer.transform.gameObject.SetActive(true);
                CheckForEnablePlayerComp();
            }
        }
        else if (!hasAuthority && SceneManager.GetActiveScene().name.StartsWith("Minimap_"))
        {
            playerNameText.enabled = true;
        }
        else if (!SceneManager.GetActiveScene().name.StartsWith("Minimap_"))
        {
            return;
        }

        if (isServer)
        {
            RpcSyncPosRot(transform.localPosition, transform.localRotation);
        }
        else
        {
            CmdSyncPosRot(transform.localPosition, transform.localRotation);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, .25f, ground);

        if (isGrounded && vel.y < 0)
        {
            vel.y = -2f;
            anim.SetBool("Jump", false);
        }

        if(!isGrounded)
            vel.y += gravity * Time.deltaTime;

        controller.Move(vel * Time.deltaTime);

        if (inventoryObject != null && hasObjectInHand)
        {
            inventoryObject.transform.position = hand.transform.position;
            inventoryObject.GetComponent<BoxCollider>().enabled = false;

        }
    }
}

