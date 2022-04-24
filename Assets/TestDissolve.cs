using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDissolve : MonoBehaviour
{
    Material mat;
    public float fade = 0f;

    bool isDessolving = false;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDessolving)
        {
            fade += Time.deltaTime;

            if(fade >= 1f)
            {
                fade = 1f;
                isDessolving = false;
            }

            mat.SetFloat("_Dissolve", fade);    
        }
    }

    public void Die()
    {
        isDessolving = true;
        Debug.Log(isDessolving);
    }
}
