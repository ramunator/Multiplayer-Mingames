using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance { get; private set; }

    public List<Reciepe> orders = new List<Reciepe>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }


}
