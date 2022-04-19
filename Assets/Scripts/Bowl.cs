using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour, IPickup
{

    public GameObject pickup { get; set; }

    public GameObject pickupGameobject;

    PlayerController player;


    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        pickup = pickupGameobject;
    }

    public void Pickup(GameObject Object)
    {
        Object = this.pickup;
        if (player.inventoryObject != null || player.Inventory != null) { return; }
        player.Inventory = this;
        player.inventoryObject = this.gameObject.transform;
    }

    public void Drop()
    {
        player.Inventory = null;
    }

    public virtual void OnDrop()
    {
        Debug.Log("Not Overrded");
    }
}
