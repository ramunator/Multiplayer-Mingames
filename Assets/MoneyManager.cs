using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public int cashGoal;
    public int minCashGoal, maxCashGoal;

    // Start is called before the first frame update
    void Start()
    {
        cashGoal = Random.Range(minCashGoal, maxCashGoal);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.cash >= cashGoal)
        {
            WinManager.Win();
        }
    }

    
}
