using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObject : MonoBehaviour
{
    public static PlacedObject Create(Vector3 worldPos, Vector2Int origin, Bulding.Dir dir, Bulding bulding)
    {
        Transform placedObjectTransform = Instantiate(bulding.prefab, worldPos, Quaternion.Euler(0, bulding.GetRotationAngle(dir), 0));

        PlacedObject placedObject = placedObjectTransform.GetComponent<PlacedObject>();

        placedObject.building = bulding;
        placedObject.origin = origin;
        placedObject.dir = dir;

        return placedObject;
    }

    private Bulding building;
    private Vector2Int origin;
    private Bulding.Dir dir;

    public List<Vector2Int> GetGridPosList()
    {
        return building.GetGridPosList(origin, dir);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

}
