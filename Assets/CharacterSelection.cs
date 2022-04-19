using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSelection : MonoBehaviour
{
    private SelectPlayer player;

    public List<Material> playerMats  = new List<Material>();

    // Start is called before the first frame update
    void Start()
    {
        player = transform.GetChild(0).GetChild(0).GetComponent<SelectPlayer>();
    }

    public void SelectPlayerMat(int id)
    {   
        PlayerManger.playerMatID = id;
        player.playerMesh.material = playerMats[id];
        PlayerPrefs.SetInt("PlayerMatId", PlayerManger.playerMatID);
    }

    public void SelectRightPlayerMat()
    {
        if (PlayerManger.playerMatID + 1 >= playerMats.Count)
        {
            PlayerManger.playerMatID = -1;
        }
        SelectPlayerMat(PlayerManger.playerMatID + 1);

        SaveSystemJson.UpdatePlayerData();
    }

    public void SelectLeftPlayerMat()
    {
        if (PlayerManger.playerMatID - 1 <= playerMats.Count)
        {
            PlayerManger.playerMatID = 13;
        }
        SelectPlayerMat(PlayerManger.playerMatID - 1);

        SaveSystemJson.UpdatePlayerData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
