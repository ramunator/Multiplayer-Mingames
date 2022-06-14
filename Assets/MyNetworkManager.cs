using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using Steamworks;
using System.IO;

public class MyNetworkManager : NetworkManager
{
    [SerializeField] private playerObjectController GamePlayerPrefab;
    [SerializeField] private ChatBehaiuver chatObject;
    public List<playerObjectController> GamePlayers { get; } = new List<playerObjectController>();
    [Scene] public List<string> maps = new List<string>();

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            //Debug.Log(File.ReadAllText("C:/Programing/Unity/Projects/SythPolygonTest/WorldData.dat"));



            playerObjectController GamePlayerInstance = Instantiate(GamePlayerPrefab);
            
            GamePlayerInstance.connectionId = conn.connectionId;
            GamePlayerInstance.playerNumberId = GamePlayers.Count + 1;
            GamePlayerInstance.playerSteamId = (ulong)SteamMatchmaking.GetLobbyMemberByIndex((CSteamID)SteamLobbyManager.Instance.currentLobbyId, GamePlayers.Count);

            NetworkServer.AddPlayerForConnection(conn, GamePlayerInstance.gameObject);
            ChatBehaiuver chatInstance = Instantiate(chatObject);

            chatInstance.lobbyId = SteamLobbyManager.Instance.currentLobbyId;


            NetworkServer.Spawn(chatInstance.gameObject);

            PlayerSpawnManager.SearchForSpawns();
            PlayerSpawnManager.PlayerSpawnPos(PlayerSpawnManager.SpawnState.Order, GamePlayerInstance.gameObject);
        }
        if (SceneManager.GetActiveScene().name.StartsWith("Minimap_"))
        {
            playerObjectController GamePlayerInstance = Instantiate(GamePlayerPrefab);

            GamePlayerInstance.connectionId = conn.connectionId;
            GamePlayerInstance.playerNumberId = GamePlayers.Count + 1;
            GamePlayerInstance.playerSteamId = (ulong)SteamMatchmaking.GetLobbyMemberByIndex((CSteamID)SteamLobbyManager.Instance.currentLobbyId, GamePlayers.Count);

            NetworkServer.AddPlayerForConnection(conn, GamePlayerInstance.gameObject);

            PlayerSpawnManager.SearchForSpawns();
        }

        if (TabManager.Instance != null)
        {
            Debug.Log(TabManager.Instance);
            TabManager.Instance.AddPlayerTab();
        }
    }

    public void StartGame()
    {
        if(MapManager.Instance.Map.Length > 0)
        {
            Debug.Log("Chaning To Selected Map");
            ServerChangeScene(MapManager.Instance.Map);
            return;
        }

        Debug.Log("Chaning To Random Map");
        ServerChangeScene(GenerateRandomMap());
        PlayerSpawnManager.SearchForSpawns();
    }
    
    public string GenerateRandomMap()
    {
        int randomMap = Random.Range(0, maps.Count);
        return maps[randomMap];
    }
}
