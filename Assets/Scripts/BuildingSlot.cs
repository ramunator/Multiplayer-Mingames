using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingSlot : MonoBehaviour
{
    public Bulding building;
    [SerializeField] private GridBuildingSystem buildingSystem;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = building.icon;
        transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = building.buildingName;
    }

    public void SetBuilding()
    {
        buildingSystem.building = this.building;
    }
}
