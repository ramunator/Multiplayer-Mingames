using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetFood : NetworkBehaviour, IPickup, IServeable
{
    public GameObject pickup { get; set; }

    public Reciepe reciepe;
    [SerializeField] private NetworkPlayerController player;

    public override void OnStartClient()
    {
        player = FindObjectOfType<NetworkPlayerController>();
    }

    public void Pickup(GameObject Object)
    {
        Object = this.pickup;
        player.Inventory = this;
    }

    public void Drop()
    {
        player.Inventory = null;

        OnDrop();
    }

    public virtual void OnDrop()
    {
        Debug.Log("Dropped Sucess");
    }

    // Start is called before the first frame update
    void Start()
    {
        pickup = this.gameObject;
        player = FindObjectOfType<NetworkPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Serve()
    {
        player.Inventory = null;
    }
}
