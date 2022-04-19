using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour, IPickup, IServeable, IWater
{
    public GameObject pickup { get; set; }
    public GameObject water { get; set; }

    public Reciepe reciepe;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject waterGO;

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

    private void Awake()
    {
        water = this.waterGO;
        pickup = this.gameObject;
        player = FindObjectOfType<PlayerController>();
    }

    public void Serve()
    {
        player.Inventory = null;
    }

    public void Fill()
    {
        water.gameObject.SetActive(true);
        Debug.Log("Filling The Glass");
    }

    public void ClearGlass()
    {
        water.gameObject.SetActive(false);
        Debug.Log("Clearing The Water");
    }
}

public interface IWater
{
    public GameObject water { get; set; }

    public void Fill();
    public void ClearGlass();
}
