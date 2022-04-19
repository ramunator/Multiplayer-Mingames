using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MicroWave : MonoBehaviour
{
    [SerializeField] private float minPlayerDistanceToOpen;
    [SerializeField] private Transform microWaveObjectPlace;

    [SerializeField] private float playerDistance;

    public InputMaster controls;
    Animator anim;
    PlayerController player;

    private void Awake()
    {
        controls = new InputMaster();

        controls.Player.Pickup.performed += ctx => OpenMicrowave();

        controls.Player.Serve.performed += ctx => PlaceObject();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        anim = GetComponent<Animator>();
    }

    void PlaceObject()
    {
        if(player.inventoryObject == null) { return; }

        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (minPlayerDistanceToOpen > playerDistance)
        {
            if (anim.GetBool("IsOpen"))
            {
                player.inventoryObject.parent = microWaveObjectPlace;
                player.inventoryObject.transform.position = microWaveObjectPlace.position;
                player.inventoryObject.GetComponent<Rigidbody>().isKinematic = true;
                player.inventoryObject.rotation = Quaternion.Euler(0, 90, 0);

                player.RemoveObjectInHand  ();
            }
        }
    }

    void OpenMicrowave()
    {
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (minPlayerDistanceToOpen > playerDistance) 
        {
            if (anim.GetBool("IsOpen"))
            {
                anim.SetBool("IsOpen", false);
            }
            else if(!anim.GetBool("IsOpen"))
            {
                anim.SetBool("IsOpen", true);
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
