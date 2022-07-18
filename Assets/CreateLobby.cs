using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Mirror;
using UnityEngine.UI;
using TMPro;

public class CreateLobby : MonoBehaviour
{
    public TMP_Text maxPlayersText;
    public static int MaxPlayers;
    public Slider maxPlayersSlider;

    public TMP_Text lobbyTypeText;
    public ELobbyType lobbyType;
    public int lobbyIndex;

    public string lobbyName;
    public TMP_InputField lobbyNameInputField;

    public GameObject leftButton;
    public GameObject rightButton;

    public void ChangeLobbyTypeLeft()
    {
        if(lobbyIndex > 0)
        {
            lobbyIndex--;
            SteamLobbyManager.Instance.lobbyType = lobbyType;
        }
    }

    public void ChangeLobbyName(string newName)
    {
        lobbyName = newName;
        SteamLobbyManager.Instance.lobbyName = lobbyName;
    }

    public void ChangeLobbyTypeRight()
    {   
        if(lobbyIndex < 2)
        {
            lobbyIndex++;
            SteamLobbyManager.Instance.lobbyType = lobbyType;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        maxPlayersText.text = maxPlayersSlider.value.ToString();
    }

    public void ChangeMaxPlayers()
    {
        SteamLobbyManager.Instance.maxPlayers = MaxPlayers;
        MaxPlayers = (int)maxPlayersSlider.value;
        maxPlayersText.text = maxPlayersSlider.value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(SteamLobbyManager.Instance == null) { return; }
        if(lobbyIndex == 0)
        {
            leftButton.SetActive(false);
            lobbyType = ELobbyType.k_ELobbyTypePrivate;
            SteamLobbyManager.Instance.lobbyType = lobbyType;
            lobbyTypeText.text = "Private";
        }
        else if (lobbyIndex == 1)
        {
            leftButton.SetActive(true);
            rightButton.SetActive(true);
            lobbyType = ELobbyType.k_ELobbyTypeFriendsOnly;
            lobbyTypeText.text = "Freinds Only";
        }
        else if (lobbyIndex == 2)
        {
            rightButton.SetActive(false);
            lobbyType = ELobbyType.k_ELobbyTypePublic;
            lobbyTypeText.text = "Public";
        }
    }
}
