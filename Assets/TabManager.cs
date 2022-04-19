using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Steamworks;
using TMPro;
using UnityEngine.InputSystem;

public class TabManager : NetworkBehaviour
{ 
    public static TabManager Instance { get; private set; }

    public Transform playerTabPrefab;
    public Transform playerTabContent;

    private void Awake()
    {
        if(Instance == null) { Instance = this; }
    }

    [ClientRpc]
    public void AddPlayerTab()
    {
        var playerTabInstance = Instantiate(playerTabPrefab, playerTabContent);

        int ret = SteamFriends.GetLargeFriendAvatar(SteamUser.GetSteamID());

        playerTabInstance.Find("Profile Pic").GetComponent<RawImage>().texture = GetSteamImageAsTexture2D(ret);
        playerTabInstance.Find("Name").GetComponent<TMP_Text>().text = SteamFriends.GetPersonaName();

        NetworkServer.Spawn(playerTabInstance.gameObject);
    }

    public static Texture2D GetSteamImageAsTexture2D(int iImage)
    {
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

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.tabKey.IsPressed())
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }
        else 
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false); 
        }
        
    }
}
