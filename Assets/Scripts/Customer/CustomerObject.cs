using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;
using System.Linq;


public class CustomerObject : MonoBehaviour
{
    public Customer customer;

    [SerializeField] private float speed;
    [SerializeField] private SkinnedMeshRenderer customerMeshRenderer;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Order order;
    [SerializeField] private GameObject tray;
    [SerializeField] private LayerMask tableLayer;

    [SerializeField] private List<Material> customerMats = new List<Material>();
    [SerializeField] private List<GameObject> stool = new List<GameObject>();
    [SerializeField] private List<GameObject> dinnerStool = new List<GameObject>();


    [HideInInspector] public Rigidbody rb;

    public NavMeshAgent agent;
    Transform player;
    [HideInInspector] public Animator anim;

    [SerializeField] Vector3 sitPos;

    [SerializeField] private Stool targetedStool;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
        stool = GameObject.FindGameObjectsWithTag("Stool").ToList();
        dinnerStool = GameObject.FindGameObjectsWithTag("DinnerStool").ToList();
        rb = GetComponent<Rigidbody>();
        agent  = GetComponent<NavMeshAgent>();
        customerMeshRenderer.material = customerMats[Random.Range(0, customerMats.Count)];

        anim.SetBool("IsWalking", true);

        for (int i = 0; i < stool.Count; i++)
        {
            int index = Random.Range(0, stool.Count);
            if (!stool[index].GetComponent<Stool>().isUsed)
            {
                agent.SetDestination(stool[index].transform.position);
                stool[index].GetComponent<Stool>().isUsed = true;
                targetedStool = stool[index].GetComponent<Stool>();

                break;
            }
        }

        nameText.text = customer.customerName;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stool") && !order.hasGotFood && other.transform.GetChild(0).childCount < 1 && targetedStool.gameObject == other.gameObject)
        {
            sitPos.y = other.transform.GetChild(0).position.y;
            Sit();
            transform.parent = other.transform.GetChild(0);
            transform.position = other.transform.GetChild(0).position;

            
        }
        else if(other.CompareTag("DinnerStool") && order.hasGotFood && other.transform.GetChild(0).childCount < 1)
        {
            sitPos.y = other.transform.GetChild(0).position.y;
            transform.parent = other.transform.GetChild(0);
            Sit();

            transform.position = other.transform.GetChild(0).position;
            tray.transform.parent = null;
            RaycastHit hit;
            Physics.Raycast(new Vector3(tray.transform.position.x - .1f, tray.transform.position.y + .5f, tray.transform.position.z), Vector3.down, out hit);
            Debug.DrawRay(new Vector3(tray.transform.position.x - .1f, tray.transform.position.y + .5f, tray.transform.position.z), Vector3.down, Color.blue, 15f);
            tray.transform.position = hit.point;
            
        }
    }

    private void Sit()
    {

        agent.Stop();
        rb.isKinematic = true;
        anim.SetBool("IsWalking", false);
        transform.position = new Vector3(0, transform.position.y, 0);
        agent.baseOffset = sitPos.y;




        order.isSitting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (order.hasGotFood && !order.isSitting)
        {
            agent.isStopped = false;
            agent.SetDestination(dinnerStool[Random.Range(0, dinnerStool.Count)].transform.position);
        }
    }
}
