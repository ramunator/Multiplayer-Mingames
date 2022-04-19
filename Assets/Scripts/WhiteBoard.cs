using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WhiteBoard : MonoBehaviour
{
    public static WhiteBoard Instance { get; private set; }

    public static List<Order> Orders = new List<Order>();

    private static List<GameObject> orderInstances = new List<GameObject>();

    [SerializeField] private Transform slots;
    [SerializeField] private Transform orderSlotPf;

    private void Start()
    {
        Instance = this;
    }

    public static void AddOrder(Order order)
    {
        Instance.AddOrderSlot(order);
        Orders.Add(order);
    }

    public static void RemoveOrder(Order order)
    {
        Orders.Remove(order);
        Instance.RemoveOrderSlot(order);
    }

    private void AddOrderSlot(Order order)
    {
        var orderInstance = Instantiate(orderSlotPf.gameObject, transform.position, Quaternion.identity, slots);

        Transform orderIconParent = orderInstance.transform.GetChild(2).transform;
        TMP_Text orderText = orderInstance.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>();
        
        orderText.text = order.currentOrder.reciepeName;
        var orderIcon = Instantiate(order.currentOrder.reciepeIconPf, new Vector3(0, 0, 0), Quaternion.identity, orderIconParent);
        orderIcon.transform.localPosition = Vector3.zero;

        orderInstances.Add(orderInstance);
    }

    private void RemoveOrderSlot(Order order)
    {
        foreach (var item in orderInstances)
        {
            Destroy(item.gameObject);
        }
    }

    private void Update()
    {
        
    }
}
