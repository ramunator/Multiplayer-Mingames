using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopGrid : MonoBehaviour
{
    //public int x;
    //public int z;

    //public GameObject originPos;
    //public float cellSize = 5f;
    //public LayerMask ground;

    //private Grid grid;

    //private void Start()
    //{
    //    grid = new Grid(x, z, cellSize, originPos.transform.position);
    //}

    //private void Update()
    //{
    //    if (Mouse.current.leftButton.isPressed)
    //    {
    //        grid.SetValue(GetMouseWorldPos(), 56);
    //    }
    //    if (Mouse.current.rightButton.isPressed)
    //    {
    //        Debug.Log(grid.GetValue(GetMouseWorldPos()));
    //    }
    //}

    //public Vector3 GetMouseWorldPos()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
    //    Physics.Raycast(ray, out RaycastHit hit, ground);
    //    Debug.Log(hit.collider.gameObject.name);
    //    return hit.point;
    //}

    //public static Vector3 GetMouseWorldPosWithZ(Vector3 screenPos, Camera worldCamera)
    //{
    //    Vector3 worldPos = worldCamera.ScreenToWorldPoint(screenPos);
    //    return worldPos;
    //}

}
