using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainMenu : MonoBehaviour
{
    public SelectPlayer currentPlayer;

    public SkinnedMeshRenderer playerMesh;

    void Update()
    {
        playerMesh.sharedMesh = currentPlayer.currentPlayer.playerObjectPf;
    }
}
