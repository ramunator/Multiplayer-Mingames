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

    bool isTyping = false;


    public static event Action<string> OnMessage;

    private void Awake()
    {
        Instance = this;

        chatManager = ChatManager.Instance;
        Debug.Log(chatManager);
    }


    private void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            inputField.gameObject.SetActive(true);
        }
    }

    [ClientCallback]
    private void OnDestroy()
    {
        if (!hasAuthority) { return; }
        
    }


    [Command(requiresAuthority =false)]
    private void CmdSendMessage(string message)
    {
        HandleMessage($"<color=yellow>[{SteamFriends.GetPersonaName()}] <color=white>{message}");
    }

    [Client]
    public void Send(string message)
    {
        if (!Keyboard.current.enterKey.wasPressedThisFrame) { return; }

        if (string.IsNullOrWhiteSpace(message)) { return; }

        CmdSendMessage(message);

        inputField.text = string.Empty;
        isTyping = false;
    }

    private void HandleMessage(string message)
    {
        chatManager.RpcSendLobbyMsg($"\n{message}");
    }
}