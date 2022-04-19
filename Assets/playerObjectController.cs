using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;

public class playerObjectController : NetworkBehaviour
{
    [SyncVar] public int connectionId;
    [SyncVar] public int playerNumberId;
    [SyncVar] public ulong playerSteamId;
    [SyncVar(hook =nameof(PlayerNameUpdate))] public string playerName;
    [SyncVar(hook =nameof(PlayerReadyUpdate))] public bool ready;

    private MyNetworkManager networkManager;

    private MyNetworkManager NetworkManager
    {
        get
        {
            if(networkManager != null)
            {
                return networkManager;
            }
            return networkManager = MyNetworkManager.singleton as MyNetworkManager;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public override void OnStartAuthority()
    {
        CmdSetPlayerName($"{SteamFriends.GetPersonaName()}");
        gameObject.name = "LocalGamePlayer";
        LobbyController.Instance.FindLocalPlayer();
        LobbyController.Instance.UpdateLobbyName();
    }

    public override void OnStartClient()
    {
        NetworkManager.GamePlayers.Add(this);
        LobbyController.Instance.UpdateLobbyName();
        LobbyController.Instance.UpdatePlayerList();
    }

    public override void OnStopClient()
    {
        NetworkManager.GamePlayers.Remove(this);
        LobbyController.Instance.UpdatePlayerList();
    }

    [Command]
    private void CmdSetPlayerName(string playerName)
    {
        this.PlayerNameUpdate(this.playerName, playerName);
    }    

    private void PlayerNameUpdate(string oldName, string newName)
    {
        if (isServer)
        {
            this.playerName = newName;
        }
        if (isClient)
        {
            LobbyController.Instance.UpdatePlayerList();
        }
    }

    private void PlayerReadyUpdate(bool oldValue, bool newValue)
    {
        if (isServer)
        {
            this.ready = newValue;
        }
        if (isClient)
        {
            LobbyController.Instance.UpdatePlayerList();
        }
    }

    [Command]
    private void CmdSetReady()
    {
        this.PlayerReadyUpdate(this.ready, !this.ready);
    }

    public void ChangeReady()
    {
        if (hasAuthority)
        {
            CmdSetReady();
        }
    }

    public void CanStartGame()
    {
        if (hasAuthority)
        {
            CmdCanStartGame();
        }
    }

    [Command]
    public void CmdCanStartGame()
    {
        NetworkManager.StartGame();
    }
}
