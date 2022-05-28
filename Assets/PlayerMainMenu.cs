using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainMenu : MonoBehaviour
{
    public SelectPlayer currentPlayer;

    void Update()
    {
        transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMesh = currentPlayer.currentPlayer.playerObjectPf;
    }
}
