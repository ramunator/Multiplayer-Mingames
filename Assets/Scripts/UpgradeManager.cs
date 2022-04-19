using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    [SerializeField] private GridBuildingSystem gridBuildingSystem;
    public BuyGrid selectedGridSize;

    public Ground ground;

    public int gridWidth;
    public int gridHeight;

    Vector3 groundScale;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(selectedGridSize != null)
        {
            selectedGridSize.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = "Selected";
        }
        if (selectedGridSize == null) { return; }
    }

}
