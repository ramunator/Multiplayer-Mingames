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

        Directory.CreateDirectory(Application.dataPath + "/Json");

        playerData = new PlayerData();

        string json = JsonUtility.ToJson(playerData);

        LoadPlayerData();
    }

    

    public static void SavePlayerData()
    {
        Instance.playerData.playerId = PlayerManger.PlayerId;
        Instance.playerData.playerMatId = PlayerManger.playerMatID;

        string json = JsonUtility.ToJson(Instance.playerData);
        Debug.Log(json);

        if (!File.Exists(Application.dataPath + "/Json/saveFile.json"))
        {
            File.Open(Application.dataPath + "/Json/saveFile.json", FileMode.OpenOrCreate);
        }

        File.WriteAllText(Application.dataPath + "/Json/saveFile.json", json);
    }

    public static void LoadPlayerData()
    {
        if(File.Exists(Application.dataPath + "/Json/saveFile.json"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/Json/saveFile.json");
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(saveString);

            PlayerManger.PlayerId = playerData.playerId;
            PlayerManger.playerMatID = playerData.playerMatId;
            Debug.Log(saveString);
        }
        else
        {
            File.Open(Application.dataPath + "/Json/saveFile.json", FileMode.OpenOrCreate);
        }
    }
}
public class PlayerData
{
    public int playerId;
    public int playerMatId;
}