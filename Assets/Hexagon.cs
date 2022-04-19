using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Hexagon : NetworkBehaviour
{
    public Color color;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<MeshRenderer>().material.color = color;
    }
    
    public IEnumerator RpcUseHexagon()
    {
        yield return new WaitForSeconds(1);

        Debug.Log("Test");
        var PlayerPos = transform.position;
        LeanTween.moveY(gameObject, -1f, 1f);

        yield return new WaitForSeconds(2);

        LeanTween.moveY(gameObject, 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
