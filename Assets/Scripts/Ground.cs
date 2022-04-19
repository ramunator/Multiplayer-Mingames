using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private UpgradeManager upgradeManager; 
    [SerializeField] private GridBuildingSystem gridBuildingSystem; 

    private void Awake()
    {
        Ground[] grounds = FindObjectsOfType<Ground>();
        foreach (var item in grounds)
        {
            Debug.Log(item.gameObject);
        }
    }

    private void Update()
    {
        if(upgradeManager.selectedGridSize == null) { return; }
        if (transform.localScale.x != upgradeManager.selectedGridSize.width | transform.localScale.z != upgradeManager.selectedGridSize.height)
        {
            transform.localScale = new Vector3(upgradeManager.selectedGridSize.width, transform.localScale.y, upgradeManager.selectedGridSize.height);
        }
    }
}