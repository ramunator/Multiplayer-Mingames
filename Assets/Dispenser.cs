using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour
{

    [SerializeField] private float minPlayerDistanceToOpen;

    [SerializeField] private float playerDistance;

    public InputMaster controls;
    public GameObject water;
    PlayerController player;

    private bool isOpen;

    private void Awake()
    {
        controls = new InputMaster();

        controls.Player.Pickup.performed += ctx => StartDispencer();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

    }

    IEnumerator FillGlass()
    {
        if (player.inventoryObject == null) { yield return null; }

        yield return new WaitForSeconds(1);
        player.inventoryObject.GetComponent<IWater>().Fill();
        
    }

    void StartDispencer()
    {
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (minPlayerDistanceToOpen > playerDistance)
        {
            if(player.inventoryObject == null) { return; }
            if (player.inventoryObject.GetComponent<Glass>())
            {
                if (isOpen)
                {
                    water.gameObject.SetActive(false);
                    isOpen = false;
                    StartCoroutine(FillGlass());
                }
                else
                {
                    water.gameObject.SetActive(true);
                    isOpen = true;
                    StartCoroutine(FillGlass());
                }
            }
        }
        else
        {
            return;
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
