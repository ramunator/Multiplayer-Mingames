using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerSpawnManager : NetworkBehaviour
{
    public enum SpawnState
    {
        Random,
        Order
    }

    public static PlayerSpawnManager Instance { get; private set; }

    public List<PlayerNetworkSpawn> playerSpawns = new List<PlayerNetworkSpawn>();
    [SyncVar] public int playersSpawned;

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
            player.transform.localPosition = Instance.playerSpawns[Random.Range(0, Instance.playerSpawns.Count)].spawn.localPosition;
            Instance.playersSpawned += 1;
        }
        else if (spawnState == SpawnState.Order)
        {
            player.transform.localPosition = Instance.playerSpawns[Instance.playersSpawned].spawn.localPosition;
            Instance.playersSpawned += 1;
        }
    }

}
