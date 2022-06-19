using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using UnityEngine.Animations.Rigging;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class playerObjectController : NetworkBehaviour
{
    [SyncVar] public int connectionId;
    [SyncVar] public int playerNumberId;
    [SyncVar] public ulong playerSteamId;
    [SyncVar(hook =nameof(PlayerNameUpdate))] public string playerName;
    [SyncVar(hook =nameof(PlayerReadyUpdate))] public bool ready;

    public Gun gun;
    public Transform rig;
    public TwoBoneIKConstraint leftHandRig;
    public TwoBoneIKConstraint rightHandRig;

    public TMP_Text playerNameText;
    public TMP_Text playerStatsNameText;
    public TMP_Text playerStatsSteamIDText;
    public RawImage playerStatsProfilePic;
    public RawImage playerIcon;

    bool avatarReceived = false;

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


    protected Callback<AvatarImageLoaded_t> ImageLoaded;

    public void SetPlayerValues()
    {
        playerStatsSteamIDText.text = playerSteamId.ToString();
        if (!avatarReceived)
        {
            GetPlayerIcon();
        }
        playerStatsNameText.text = playerName;
    }
    
    void GetPlayerIcon()
    {
        Debug.Log("Trying To Get Player Profile");
        int imageId = SteamFriends.GetLargeFriendAvatar((CSteamID)playerSteamId);
        if (imageId == -1) { Debug.LogError("Player Image Error"); return; }
        playerIcon.texture = GetSteamImageAsTexture(imageId);
        playerStatsProfilePic.texture = GetSteamImageAsTexture(imageId);
    }

    private void OnImageLoaded(AvatarImageLoaded_t callback)
    {
        if (callback.m_steamID.m_SteamID == playerSteamId)
        {
            Debug.Log("Got Profile Picture");
            playerIcon.texture = GetSteamImageAsTexture(callback.m_iImage);
        }
        else
        {
            Debug.LogError("Failed To Get Profile Picture");
            return;
        }
    }

    private Texture2D GetSteamImageAsTexture(int iImage)
    {
        Texture2D texture = null;

        bool isValid = SteamUtils.GetImageSize(iImage, out uint width, out uint height);
        if (isValid)
        {
            byte[] image = new byte[width * height * 4];

            isValid = SteamUtils.GetImageRGBA(iImage, image, (int)(width * height * 4));

            if (isValid)
            {
                texture = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false, true);
                texture.LoadRawTextureData(image);
                texture.Apply();
            }
        }
        else
        {
            Debug.LogError("Failed To Load Texture");
        }
        avatarReceived = true;
        return texture;
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        ImageLoaded = Callback<AvatarImageLoaded_t>.Create(OnImageLoaded);
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
            playerNameText.text = playerName;
            playerStatsNameText.text = playerName;
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
