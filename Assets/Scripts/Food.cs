using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IPickup, IServeable
{
    public GameObject pickup { get; set; }

    public Reciepe reciepe;
    [SerializeField] private PlayerController player;

    

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
        player = FindObjectOfType<PlayerController>();
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
