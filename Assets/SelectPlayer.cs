using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectPlayer : MonoBehaviour
{
    public SkinnedMeshRenderer playerMesh;
    public PlayerSO currentPlayer;

    public TMP_Text currentPlayerNameText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        currentPlayerNameText.text = currentPlayer.name;
    }
}
