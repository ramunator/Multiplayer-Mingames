using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickup
{
    public GameObject pickup { get; set; }

    public void Pickup(GameObject Object);
    public void Drop();
    public void OnDrop();
}