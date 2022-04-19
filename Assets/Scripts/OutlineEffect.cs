using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineEffect : MonoBehaviour
{
    public bool triggerOutline;
    public Material outlineMat;

    // Start is called before the first frame update
    void Start()
    {
        if (triggerOutline)
        {
            GetComponent<MeshRenderer>().material = outlineMat;
        }
    }

    public void TriggerHolo()
    {
        GetComponent<MeshRenderer>().material = outlineMat;
    }
}
