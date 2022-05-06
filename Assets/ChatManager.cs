using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;

public class ChatManager : NetworkBehaviour
{
    public static ChatManager Instance { get; private set; }



    private void Awake()
    {
        gameObject.SetActive(true);
        if (Instance == null) { Instance = this; }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
