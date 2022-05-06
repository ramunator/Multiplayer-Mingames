using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private List<Customer> customers = new List<Customer>();
    [SerializeField] private List<Transform> spawnPos = new List<Transform>();

    bool timeForNxtCustomer = true;

    IEnumerator SpawnCustomer()
    {
        int _customer = Random.Range(0, customers.Count);
        Transform spawnPosTransform = spawnPos[Random.Range(0, spawnPos.Count)];
        GameObject customerInstance = Instantiate(
            customers[_customer].pfModel,
            spawnPosTransform.position, Quaternion.identity, spawnPosTransform);

        Customer customerSO = customerInstance.GetComponent<CustomerObject>().customer;

        customerSO = customers[_customer];
        if(customerSO.gender == Customer.Gender.Boy || customerSO.gender == Customer.Gender.Man)
        {
            customerSO.customerName = CustomerNames.BoyCustomerNames[ Random.Range(0, CustomerNames.BoyCustomerNames.Length)];
        }
        if(customerSO.gender == Customer.Gender.Girl || customerSO.gender == Customer.Gender.Women)
        {
            customerSO.customerName = CustomerNames.GirlCustomerNames[Random.Range(0, CustomerNames.GirlCustomerNames.Length)];
        }

        yield return new WaitForSeconds(Random.Range(12, 25));
        timeForNxtCustomer = true;
    }

    private void Update()
    {
        if (timeForNxtCustomer)
        {
            timeForNxtCustomer = false;
            StartCoroutine(SpawnCustomer());
        }
    }

}