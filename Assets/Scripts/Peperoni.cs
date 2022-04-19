using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

public class Peperoni : MonoBehaviour, IIngradiant
{
    public InputMaster controls;

    public LayerMask ingradiantHolderLayer;

    public float checkRadius;

    PlayerController player;
    GameObject ingradiantHolder;

    public IIngradiant.Ingradient Ingradiant { get; set; }

    public void PlaceIngradiant()
    {
        player.Inventory = null;
        player.inventoryObject = null;
        Destroy(gameObject);
        ingradiantHolder.transform.GetChild(0).gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        controls = new InputMaster();

        controls.Player.Pickup.performed += ctx => CheckPlaceIngradiants();
    }

    private void CheckPlaceIngradiants()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, checkRadius, transform.forward, out hit, 20f, ingradiantHolderLayer))
        {
            ingradiantHolder = hit.collider.gameObject;
            PlaceIngradiant();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }

    /*private void OnGUI()
    {
        checkRadius = EditorGUILayout.PropertyField("Check Radius:", GUILayout.Width(10f), GUILayout.Height(20f));
    }*/

}

public class GuiTest : ScriptableObject
{
    public string test = "Test";
}
