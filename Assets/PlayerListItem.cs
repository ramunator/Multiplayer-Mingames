using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;
using TMPro;
using System;

public class PlayerListItem : MonoBehaviour
{
    public string playerName;
    public int connectionId;
    public ulong playerSteamId;
    private bool avatarReceived;

    public TMP_Text playerNameText;
    public RawImage playerIcon;
    public TMP_Text readyText;
    public bool ready;

    protected Callback<AvatarImageLoaded_t> ImageLoaded;

    public void ChangeReadyStatus()
    {
        if (ready)
        {
            readyText.text = "<color=green>Ready";
        }
        else
        {
            readyText.text = "<color=red>Not Ready";
        }
    }

    private void Start()
    {
        ImageLoaded = Callback<AvatarImageLoaded_t>.Create(OnImageLoaded);
    }

    public void SetPlayerValues()
    {
        playerNameText.text = playerName;
        ChangeReadyStatus();
        if (!avatarReceived)
        {
            GetPlayerIcon();
        }
    }

    void GetPlayerIcon()
    {
        Debug.Log("Trying To Get Player Profile");
        int imageId = SteamFriends.GetLargeFriendAvatar((CSteamID)playerSteamId);
        if(imageId == -1) { Debug.LogError("Player Image Error"); return; }
        playerIcon.texture = GetSteamImageAsTexture(imageId);
    }

    private void OnImageLoaded(AvatarImageLoaded_t callback)
    {
        if(callback.m_steamID.m_SteamID == playerSteamId)
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
}
