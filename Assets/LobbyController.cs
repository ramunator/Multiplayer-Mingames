using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Steamworks;
using System.Linq;
using TMPro;
using System;

public class LobbyController : MonoBehaviour
{
    public static LobbyController Instance { get; private set; }

    [Header("UI")]
    public TMP_Text lobbyNameText;

    [Header("Player Data")]
    public GameObject playerListViewContent;
    public GameObject playerListItemPrefab;
    public GameObject localPlayerObject;

    [Header("Other Data")]
    public ulong currentLobbyId;
    public bool playerItemCreated = false;
    private List<PlayerListItem> playerListItems = new List<PlayerListItem>();
    public playerObjectController localPlayerObjectController;

    [Header("Ready")]
    public TMP_Text readyButtonText;
    public ButtonHover startGameButton;

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

    void Awake()
    {
        if(Instance == null) { Instance = this; }
    }

    public void ReadyPlayer()
    {
        localPlayerObjectController.ChangeReady();
    }

    public void UpdateButton()
    {
        if (localPlayerObjectController.ready)
        {
            readyButtonText.text = "<color=red>UnReady";
        }
        else
        {
            readyButtonText.text = "<color=green>Ready";
        }
    }

    public void CheckIfAllReady()
    {
        bool allReady = false;

        foreach (playerObjectController player in NetworkManager.GamePlayers)
        {
            if (player.ready)
            {
                allReady = true;
            }
            else
            {
                allReady = false;
                break;
            }
        }

        if (allReady)
        {
            if(localPlayerObjectController.playerNumberId == 1)
            {
                startGameButton.interactable = true;
            }
            else
            {
                startGameButton.interactable = false;
            }
        }
        else
        {
            startGameButton.interactable = false;
        }
    }

    public void UpdateLobbyName()
    {
        currentLobbyId = NetworkManager.GetComponent<SteamLobbyManager>().currentLobbyId;
        lobbyNameText.text = SteamMatchmaking.GetLobbyData(new CSteamID(currentLobbyId), "name");
        Debug.Log(SteamMatchmaking.GetLobbyData(new CSteamID(currentLobbyId), "name"));
    }

    public void UpdatePlayerList()
    {
        if (!playerItemCreated)
        {
            CreateHostPlayerItem();
        }
        if(playerListItems.Count < NetworkManager.GamePlayers.Count)
        {
            CreateClientPlayerItem();
        }
        if(playerListItems.Count > NetworkManager.GamePlayers.Count) { RemovePlayerItem(); }
        if(playerListItems.Count == NetworkManager.GamePlayers.Count) { UpdatePlayerItem(); }
    }

    public void CreateHostPlayerItem()
    {
        foreach (playerObjectController player in NetworkManager.GamePlayers)
        {
            GameObject newPlayerItem = Instantiate(playerListItemPrefab) as GameObject;
            PlayerListItem newPlayerItemScript = newPlayerItem.GetComponent<PlayerListItem>();

            newPlayerItemScript.playerName = player.playerName;
            newPlayerItemScript.connectionId = player.connectionId;
            newPlayerItemScript.playerSteamId = player.playerSteamId;
            newPlayerItemScript.SetPlayerValues();

            newPlayerItem.transform.SetParent(playerListViewContent.transform);
            newPlayerItem.transform.localScale = Vector3.one;

            playerListItems.Add(newPlayerItemScript);
        }

        playerItemCreated = true;
    }
    
    public void CreateClientPlayerItem()
    {
        foreach (playerObjectController player in NetworkManager.GamePlayers)
        {
            if(!playerListItems.Any(b => b.connectionId == player.connectionId))
            {
                GameObject newPlayerItem = Instantiate(playerListItemPrefab) as GameObject;
                PlayerListItem newPlayerItemScript = newPlayerItem.GetComponent<PlayerListItem>();

                newPlayerItemScript.playerName = player.playerName;
                newPlayerItemScript.connectionId = player.connectionId;
                newPlayerItemScript.playerSteamId = player.playerSteamId;
                newPlayerItemScript.ready = player.ready;
                newPlayerItemScript.SetPlayerValues();

                newPlayerItem.transform.SetParent(playerListViewContent.transform);
                newPlayerItem.transform.localScale = Vector3.one;

                playerListItems.Add(newPlayerItemScript);
            }
        }
    }

    public void UpdatePlayerItem()
    {
        foreach (playerObjectController player in NetworkManager.GamePlayers)
        {
            foreach (PlayerListItem playerListItemScript in playerListItems)
            {
                if (playerListItemScript.connectionId == player.connectionId)
                {
                    playerListItemScript.playerName = player.playerName;
                    playerListItemScript.ready = player.ready;
                    playerListItemScript.SetPlayerValues();
                    if(player == localPlayerObjectController)
                    {
                        UpdateButton();
                    }
                }
            }
        }
        CheckIfAllReady();
    }

    public void FindLocalPlayer()
    {
        localPlayerObject = GameObject.Find("LocalGamePlayer");
        localPlayerObjectController = localPlayerObject.GetComponent<playerObjectController>();
    }

    public void RemovePlayerItem()
    {
        List<PlayerListItem> playerListItemsToRemove = new List<PlayerListItem>();
        foreach (PlayerListItem playerListItem in playerListItemsToRemove)
        {
            if(!NetworkManager.GamePlayers.Any(b => b.connectionId == playerListItem.connectionId))
            {
                playerListItemsToRemove.Add(playerListItem);
            }
        }

        if(playerListItemsToRemove.Count > 0)
        {
            foreach (PlayerListItem playerListItemToRemove in playerListItemsToRemove)
            {
                GameObject objectToRemove = playerListItemToRemove.gameObject;
                playerListItems.Remove(playerListItemToRemove);
                Destroy(objectToRemove);
                objectToRemove = null;
            }
        }
    }

    public void StartGame()
    {
        localPlayerObjectController.CanStartGame();
    }
}
