using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class PlayerSpawnManager : MonoBehaviour
{
    public enum SpawnState
    {
        Random,
        Order
    }

    public static PlayerSpawnManager Instance { get; private set; }

    public List<PlayerNetworkSpawn> playerSpawns = new List<PlayerNetworkSpawn>();
    public int playersSpawned;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        SearchForSpawns();
    }

    public static void SearchForSpawns()
    {
        Instance.playerSpawns.Clear();

        foreach (PlayerNetworkSpawn spawn in FindObjectsOfType<PlayerNetworkSpawn>())
        {
            Instance.playerSpawns.Add(spawn);
        }
    }

    public static void PlayerSpawnPos(SpawnState spawnState, GameObject player)
    {
        Debug.Log("Settng Player Pos");
        if(spawnState == SpawnState.Random)
        {
            player.transform.position = Instance.playerSpawns[Random.Range(0, Instance.playerSpawns.Count)].spawn.position;
        }
        else if (spawnState == SpawnState.Order)
        {
            player.transform.position = Instance.playerSpawns[Instance.playersSpawned].spawn.position;
        }
        Instance.playersSpawned += 1;

        if (SceneManager.GetActiveScene().name.StartsWith("Minimap_"))
        {
            player.GetComponent<CharacterController>().enabled = true;
        }
    }

}
