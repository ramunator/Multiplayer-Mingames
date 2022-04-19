using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Order : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float playerCheckRadius;
    [SerializeField] private float orderTime;
    private float currentOrderTime;
    private float distanceToPlayer;
    [SerializeField] private Camera mainCamera;

    [Header("Order")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private TMP_Text orderNameText;
    public Reciepe currentOrder;
    public bool hasGotFood = false;
    public bool isSitting = false;
    public CustomerObject customerObject;

    private PlayerController player;
    private Transform customerOrigin;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        customerObject = transform.parent.parent.parent.GetComponent<CustomerObject>();
        anim = transform.parent.GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
        customerOrigin = transform.parent.parent.GetChild(0);

        currentOrder = OrderManager.Instance.orders[Random.Range(0, OrderManager.Instance.orders.Count)];

        UpdateOrder();

        WhiteBoard.AddOrder(this);
    }

    public void UpdateOrder()
    {
        orderNameText.text = currentOrder.reciepeName;
    }

    public bool PlayerisInRange()
    {
        if (distanceToPlayer <= playerCheckRadius) { return true; }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasGotFood)
        {
            currentOrderTime += Time.deltaTime;
            if(currentOrderTime >= orderTime)
            {
                Debug.Log("Didn't Get Order!!!");
                customerObject.gameObject.SetActive(false);
            }
        }

        mainCamera = Camera.main;
        transform.parent.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, Camera.main.transform.rotation.y, transform.rotation.z));

        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (PlayerisInRange())
        {
            anim.SetBool("ShowOrder", true);
        }
        else
        {
            anim.SetBool("ShowOrder", false);
        }
    }
}
