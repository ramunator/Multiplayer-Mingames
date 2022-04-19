using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour
{
    public Transform mainPos;
    public Transform drinkPos;
    public Transform extraPos;

    public GameObject mainItemPlaced;
    public GameObject drinkItemPlaced;
    public GameObject extraItemPlaced;

    private void Update()
    {
        if(mainItemPlaced != null) 
        {
            mainItemPlaced.transform.position = mainPos.position;
            mainItemPlaced.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        if(drinkItemPlaced != null) { drinkItemPlaced.transform.position = Vector3.zero; }
        if(extraItemPlaced != null) { extraItemPlaced.transform.position = Vector3.zero; }
    }
}
