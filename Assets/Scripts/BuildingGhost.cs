using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    [SerializeField] private Material ghostMat;


    [SerializeField] private Transform visual;
    [SerializeField] Bulding placedObjectTypeSO;

    private void Start()
    {
        RefreshVisual();
        


        GridBuildingSystem.Instance.OnSelectedChanged += Instance_OnSelectedChanged;
    }

    private void Instance_OnSelectedChanged(object sender, System.EventArgs e)
    {
        RefreshVisual();

    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.buildMode) { return; }
        Vector3 targetPosition = GridBuildingSystem.Instance.GetMouseWorldSnappedPosition();
        targetPosition.y = 1f;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 15f);

        transform.rotation = Quaternion.Lerp(transform.rotation, GridBuildingSystem.Instance.GetPlacedObjectRotation(), Time.deltaTime * 15f);
    }

    public void DeSelect()
    {
        RefreshVisual();
    }

    public void RefreshVisual()
    {
        if (visual != null)
        {
            Destroy(visual.gameObject);
            visual = null;
        }

        Bulding placedObjectTypeSO = GridBuildingSystem.Instance.GetPlacedObjectTypeSO();

        if (placedObjectTypeSO != null)
        {
            this.placedObjectTypeSO = placedObjectTypeSO;
            visual = Instantiate(placedObjectTypeSO.visual, Vector3.zero, Quaternion.identity);
            
            visual.parent = transform;
            visual.localPosition = Vector3.zero;
            visual.localEulerAngles = Vector3.zero;
            for (int i = 0; i < visual.GetChild(1).GetComponent<MeshRenderer>().materials.Length; i++)
            {
                visual.GetChild(1).GetComponent<MeshRenderer>().material = ghostMat;
            }


            //SetLayerRecursive(visual.gameObject, 11);


        }
    }



    private void SetLayerRecursive(GameObject targetGameObject, int layer)
    {
        targetGameObject.layer = layer;
        foreach (Transform child in targetGameObject.transform)
        {
            SetLayerRecursive(child.gameObject, layer);
        }
    }
}
