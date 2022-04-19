using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HexagonHeat : NetworkBehaviour
{
    public GameObject currentColorBoard;
    
    public List<Hexagon> hexagons = new List<Hexagon>();

    [SyncVar(hook = nameof(HandleNewColor))] public Color CurrentColor;

    float timeForNext = 5;

    Material cachedMaterial;

    public override void OnStartServer()
    {
        base.OnStartServer();

    }

    public void NewHexagon()
    {
        int randomHexagon = Random.Range(0, hexagons.Count);
        CurrentColor = hexagons[randomHexagon].color;
        for (int i = 0; i < hexagons.Count; i++)
        {
            if(i != randomHexagon) 
            {
                Debug.Log("Test");
                StartCoroutine(hexagons[i].RpcUseHexagon());
            }
        }
    }

    private void HandleNewColor(Color oldColor, Color newColor)
    {
        if (cachedMaterial == null)
            cachedMaterial = currentColorBoard.GetComponent<MeshRenderer>().material;

        cachedMaterial.color = newColor;
    }

    // Update is called once per frame
    void Update()
    {
        timeForNext += Time.deltaTime;
        if(timeForNext > 10)
        {
            NewHexagon();
            timeForNext = 0;
        }
    }
}
