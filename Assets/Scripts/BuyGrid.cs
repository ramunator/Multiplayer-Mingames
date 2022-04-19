using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyGrid : MonoBehaviour
{
    public int width;
    public int height;

    public void BuyBiggerGrid()
    {
        UpgradeManager.Instance.gridHeight = height;
        UpgradeManager.Instance.gridWidth = width;

        UpgradeManager.Instance.selectedGridSize = this;
    }
}
