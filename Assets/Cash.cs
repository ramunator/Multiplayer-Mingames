using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cash : MonoBehaviour
{
    private void Update()
    {
        transform.GetChild(0).GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("Cash").ToString();
    }
}
