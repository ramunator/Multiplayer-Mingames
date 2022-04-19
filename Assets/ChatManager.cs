using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;

public class ChatManager : NetworkBehaviour
{
    public static ChatManager Instance { get; private set; }

    public CSteamID lobbyId;

    protected Callback<LobbyEnter_t> lobbyEntered;
    protected Callback<LobbyChatMsg_t> LobbyChatMsg;
    protected Callback<LobbyCreated_t> lobbyCreated;

    string newMsg;


    public enum MessageTypeEnum
    {
        ChatMessage,
        ServerMessage,
        WhateverReallyMessage
    }

    private void Awake()
    {
        

        gameObject.SetActive(true);
        if (Instance == null) { Instance = this; }
        DontDestroyOnLoad(gameObject);
    }

    public bool SendChatMessage(string Message, CSteamID ID)
    {
        //MessageTypeEnum messageType = MessageTypeEnum.ChatMessage;
        byte[] message = new byte[1024];
        message = System.Text.Encoding.ASCII.GetBytes(Message);
        return SteamMatchmaking.SendLobbyChatMsg(ID, message, message.Length);
    }

    private void OnLobbyChatMsg(LobbyChatMsg_t callback)
    {
        byte[] Data = new byte[4096];
        int ret = SteamMatchmaking.GetLobbyChatEntry((CSteamID)callback.m_ulSteamIDLobby, (int)callback.m_iChatID, out var SteamIDUser, Data, Data.Length, out var ChatEntryType);
        
        string data = System.Text.Encoding.Default.GetString(Data);
        Debug.Log(ret + " | " + data);
        ChatBehaiuver.Instance.chatText.text = $"{data}";
    }

    // Start is called before the first frame update
    void Start()
    {
        lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
        LobbyChatMsg = Callback<LobbyChatMsg_t>.Create(OnLobbyChatMsg);
        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
    }

    [ClientRpc]
    public void SendLobbyMsg(string msg)
    {
        Debug.Log(msg);
        newMsg = msg;
        SendChatMessage(msg, lobbyId);
    }

    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        lobbyId = new CSteamID(callback.m_ulSteamIDLobby);
    }

    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        lobbyId = new CSteamID(callback.m_ulSteamIDLobby);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
