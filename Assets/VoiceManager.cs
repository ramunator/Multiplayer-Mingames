using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using System.Linq;
using UnityEngine.UI;

public class VoiceManager : NetworkBehaviour
{
    public static VoiceManager Instance { get; private set; }

    public Transform content;
    public Transform playerVoicePf;

    public List<uint> playerVoiceId = new List<uint>();

    private void Awake()
    {
        Instance = this;
    }

    [Command(requiresAuthority =false)]
    public void CmdAddPlayerVoiceGFX()
    {
        Debug.LogError("Should Add Player!");

        if (!SteamManager.Initialized) { return; }

        var playerVoiceInstance = Instantiate(Instance.playerVoicePf, Instance.content);

        playerVoiceInstance.Find("Player name").GetComponent<TMPro.TMP_Text>().text = $"{SteamFriends.GetPersonaName()} | {netId}";

        playerVoiceId.Add(netId);

        NetworkServer.Spawn(playerVoiceInstance.gameObject);

        int ret = SteamFriends.GetMediumFriendAvatar(SteamUser.GetSteamID());
        RawImage playerProfilePic = playerVoiceInstance.transform.Find("Player Icon").GetComponent<RawImage>();
        playerProfilePic.texture = GetSteamImageAsTexture2D(ret);
    }

    public static Texture2D GetSteamImageAsTexture2D(int iImage)
    {
        if (!SteamManager.Initialized) { return null; }

        Texture2D ret = null;
        uint ImageWidth;
        uint ImageHeight;
        bool bIsValid = SteamUtils.GetImageSize(iImage, out ImageWidth, out ImageHeight);

        if (bIsValid)
        {
            byte[] Image = new byte[ImageWidth * ImageHeight * 4];

            bIsValid = SteamUtils.GetImageRGBA(iImage, Image, (int)(ImageWidth * ImageHeight * 4));
            if (bIsValid)
            {
                ret = new Texture2D((int)ImageWidth, (int)ImageHeight, TextureFormat.RGBA32, false, true);
                ret.LoadRawTextureData(Image);
                ret.Apply();
            }
        }

        return ret;
    }

    public void RemovePlayerVoiceGFX()
    {
        if (!SteamManager.Initialized) { return; }

        for (int i = 0; i < playerVoiceId.Count; i++)
        {
            if(playerVoiceId[i] == netId)
            {
                if(content.GetChild(i).gameObject == null) { return; }
                Destroy(content.GetChild(i).gameObject);
                playerVoiceId.RemoveAt(i);
            }
        }
    }
}
