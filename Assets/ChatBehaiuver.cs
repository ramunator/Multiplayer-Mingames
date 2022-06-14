using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Steamworks;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class ChatBehaiuver : NetworkBehaviour
{
    public static ChatBehaiuver Instance { get; private set; }

    public ChatManager chatManager;

    [SerializeField] private GameObject chatUI;
    public TMP_Text chatText;
    [SerializeField] private TMP_InputField inputField;

    public ulong lobbyId;

    protected Callback<LobbyEnter_t> lobbyEntered;
    protected Callback<LobbyChatMsg_t> LobbyChatMsg;
    protected Callback<LobbyCreated_t> lobbyCreated;

    [SerializeField] private float spamDelay = 1.5f;

    float timeBtwLastMsg = 0f;
    float timeBtwSelected = 0f;

    bool isSelected = false;

    public enum MessageTypeEnum
    {
        ChatMessage,
        ServerMessage,
        WhateverReallyMessage
    }

    private void Awake()
    {
        Instance = this;

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

    public void IsSelected(bool isSelected)
    {
        this.isSelected = isSelected;
    }

    private void Update()
    {
        timeBtwLastMsg += Time.deltaTime;
        timeBtwSelected += Time.deltaTime;

        if (Keyboard.current.enterKey.wasPressedThisFrame && isSelected && timeBtwLastMsg > spamDelay)
        {
            Debug.Log("Send Message!");
            Send(inputField.text, MessageTypeEnum.ChatMessage);
            timeBtwLastMsg = 0;
        }
        else if (Keyboard.current.enterKey.wasPressedThisFrame && !isSelected && timeBtwSelected > .25f)
        {
            Debug.Log("Test some" + this.gameObject);
            inputField.Select();
            timeBtwSelected = 0;
        }
    }

    [ClientCallback]
    private void OnDestroy()
    {
        if (!hasAuthority) { return; }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        LobbyChatMsg = Callback<LobbyChatMsg_t>.Create(OnLobbyChatMsg);
    }

    public void SendLobbyMsg(string msg)
    {
        SendChatMessage(msg, (CSteamID)lobbyId);
    }

    private void SendChatMessageFunc(string message)
    {
        SendChatMessage($"\n<color=yellow>[{SteamFriends.GetPersonaName()}] <color=white>{message}", (CSteamID)lobbyId);
    }

    private void SendServerMessageFunc(string message)
    {
        SendChatMessage($"{message}", (CSteamID)lobbyId);
    }

    public void Send(string message, MessageTypeEnum messageType)
    {
        if (string.IsNullOrWhiteSpace(message)) { return; }

        if(messageType == MessageTypeEnum.ChatMessage)
        {
            SendChatMessageFunc(message);
        }
        else if(messageType == MessageTypeEnum.ServerMessage)
        {
            SendServerMessageFunc(message);
        }


        inputField.text = string.Empty;
    }
}