using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : NetworkBehaviour
{
    public static MapManager Instance { get; private set; }

    [SyncVar] public string Map;

    private void Awake()
    {
        Instance = this;
    }
}
