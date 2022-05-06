using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonTest : MonoBehaviour
{
    public static JsonTest Instance { get; private set; }

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

        string json = JsonUtility.ToJson(playerData);
        Debug.Log(json);

        if (File.Exists(Application.dataPath + "/Json/saveFile.json"))
        {

        }
        else
        {

        }

        File.WriteAllText(Application.dataPath + "/Json/saveFile.json", json);
    }

    public static void SavePlayerPos()
    {
        string json = JsonUtility.ToJson(Instance.playerData);
        Debug.Log(json);

        File.WriteAllText(Application.dataPath + "/Json/saveFile.json", json);
    }
}
public class PlayerDataTest
{
    public int playerId;
    public Vector3 pos;
}