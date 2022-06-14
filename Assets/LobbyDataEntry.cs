using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Steamworks;
using Mirror;

public class LobbyDataEntry : MonoBehaviour
{
    [Header("Data")]
    public CSteamID lobbyId;
    public string lobbyName;
    public TMP_Text lobbyNameText;
    public TMP_Text playersInLobby;
    private SteamLobbyManager steamLobbyManager;

    private void Start()
    {
        steamLobbyManager = NetworkManager.singleton.gameObject.GetComponent<SteamLobbyManager>();
    }

    public void SetLobbyData()
    {
        if (string.IsNullOrWhiteSpace(lobbyName))
        {
            lobbyNameText.text = "Missing Name";
        }
        else
        {
            lobbyNameText.text = lobbyName;
        }

        playersInLobby.text = $"<color=black>{SteamMatchmaking.GetNumLobbyMembers(lobbyId)} / {SteamMatchmaking.GetLobbyMemberLimit(lobbyId)}";
    }

    public void JoinLobby()
    {
        steamLobbyManager.JoinLobby(lobbyId);
    }


}
