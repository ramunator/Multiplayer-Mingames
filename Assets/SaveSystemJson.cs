using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystemJson : MonoBehaviour
{
    public static SaveSystemJson Instance { get; private set; }

    public PlayerData playerData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerData = new PlayerData();

        playerData.playerId = PlayerManger.PlayerId;
        playerData.playerMatId = PlayerManger.playerMatID;

        string json = JsonUtility.ToJson(playerData);
        Debug.Log(json);

        File.WriteAllText(Application.dataPath + "/Json/saveFile.json", json);
    }

    public static void UpdatePlayerData()
    {
        Instance.playerData.playerId = PlayerManger.PlayerId;
        Instance.playerData.playerMatId = PlayerManger.playerMatID;

        string json = JsonUtility.ToJson(Instance.playerData);
        Debug.Log(json);

        File.WriteAllText(Application.dataPath + "/Json/saveFile.json", json);
    }
}
public class PlayerData
{
    public int playerId;
    public int playerMatId;
}


