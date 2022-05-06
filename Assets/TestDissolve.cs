using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TestDissolve : NetworkBehaviour
{
    public Material dissolveMat;
    public float fade = 1f;

    bool isDessolving = false;

    // Update is called once per frame
    void Update()
    {
        if (isDessolving)
        {
            fade -= Time.deltaTime;

            if(fade <= 0f)
            {
                fade = 0f;
                isDessolving = false;
                Destroy(gameObject);
            }

            GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Progress", fade);    
        }
    }

    [Command(requiresAuthority =false)]
    public void CmdDie()
    {
        gameObject.GetComponent<SkinnedMeshRenderer>().material = dissolveMat;
        isDessolving = true;
        Debug.Log(isDessolving);
    }
}
