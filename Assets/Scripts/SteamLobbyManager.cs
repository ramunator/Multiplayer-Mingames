using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using System;
using System.IO;

public class SteamLobbyManager : MonoBehaviour
{
    public static SteamLobbyManager Instance { get; private set; }

    public string lobbyName;
    public bool useSteam;
    public int maxPlayers;
    public ELobbyType lobbyType;

    public ulong currentLobbyId;

    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;
    protected Callback<LobbyEnter_t> lobbyEntered;

    protected Callback<LobbyMatchList_t> lobbyList;
    protected Callback<LobbyDataUpdate_t> lobbyDataUpdated;

    protected Callback<LobbyKicked_t> lobbyKicked;
    protected Callback<LobbyChatMsg_t> lobbyChatMsg;

    public List<CSteamID> lobbyIds = new List<CSteamID>();

    private MyNetworkManager networkManager;

    private MyNetworkManager NetworkManager
    {
        get
        {
            if (networkManager != null)
            {
                return networkManager;
            }
            return networkManager = MyNetworkManager.singleton as MyNetworkManager;
        }
    }

    private void Start()
    {
        if(Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }

        if (useSteam)
        {
            lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
            gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
            lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);

            lobbyList = Callback<LobbyMatchList_t>.Create(OnGetLobbyList);
            lobbyDataUpdated = Callback<LobbyDataUpdate_t>.Create(OnLobbyDataUpdated);

            lobbyKicked = Callback<LobbyKicked_t>.Create(OnLobbyKicked);
            lobbyChatMsg = Callback<LobbyChatMsg_t>.Create(OnLobbyChatMsg);

            //const string hello = "Test";
            //SteamMatchmaking.SendLobbyChatMsg(new CSteamID(currentLobbyId), hello);
        }
    }

    public void HostLobby()
    {
        if (useSteam && SteamManager.Initialized)
        {
            SteamMatchmaking.CreateLobby(lobbyType, maxPlayers);
            Debug.Log("Started Game");
            return;
        }

        NetworkManager.StartHost();
        Debug.Log("Started Game");
    }

    private void OnLobbyKicked(LobbyKicked_t callback)
    {
        Debug.Log($"A Person Got Kicked");
    }

    private void OnLobbyChatMsg(LobbyChatMsg_t callback)
    {
        Debug.Log($"Recived Chat Msg");
    }

    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if(callback.m_eResult != EResult.k_EResultOK) { Debug.LogError("Lobby Failed"); return; }

        currentLobbyId = callback.m_ulSteamIDLobby;

        NetworkManager.StartHost();

        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "HostAddress", SteamUser.GetSteamID().ToString());
        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "name", lobbyName);

    }

    private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }
    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        if (NetworkServer.active) { return; }

        currentLobbyId = callback.m_ulSteamIDLobby;

        Debug.Log(callback.m_ulSteamIDLobby);
        
        string hostAddress = SteamMatchmaking.GetLobbyData(
            new CSteamID(callback.m_ulSteamIDLobby),
            "HostAddress");
        NetworkManager.networkAddress = hostAddress;
        NetworkManager.StartClient();
    }

    public void JoinLobby(CSteamID lobbyId)
    {
        SteamMatchmaking.JoinLobby(lobbyId);
    }

    public void GetLobbiesList(string lobbyName, ELobbyDistanceFilter eLobbyDistanceFilter)
    {
        if(lobbyIds.Count > 0) { lobbyIds.Clear(); }

        if(lobbyName.Length > 0) { SteamMatchmaking.AddRequestLobbyListStringFilter("name", lobbyName, ELobbyComparison.k_ELobbyComparisonEqual); }
        if(eLobbyDistanceFilter != ELobbyDistanceFilter.k_ELobbyDistanceFilterDefault) { SteamMatchmaking.AddRequestLobbyListDistanceFilter(eLobbyDistanceFilter); }
        
        SteamMatchmaking.AddRequestLobbyListResultCountFilter(30);
        SteamMatchmaking.RequestLobbyList();
    }

    private void OnGetLobbyList(LobbyMatchList_t callback)
    {
        if(LobbyListManager.Instance.LobbyList.Count > 0) { LobbyListManager.Instance.DestroyLobby(); }

        for (int i = 0; i < callback.m_nLobbiesMatching; i++)
        {
            CSteamID lobbyId = SteamMatchmaking.GetLobbyByIndex(i);
            lobbyIds.Add(lobbyId);
            SteamMatchmaking.RequestLobbyData(lobbyId);
        }
    }

    private void OnLobbyDataUpdated(LobbyDataUpdate_t callback)
    {
        LobbyListManager.Instance.DisplayLobby(lobbyIds, callback);
    }
}
