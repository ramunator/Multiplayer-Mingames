using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMap : NetworkBehaviour
{
    [Scene] public string map;

    public MapManager mapManager;

    [Command(requiresAuthority =false)]
    public void SelectMapButton()
    {
        if(map != null)
        {
            mapManager.Map = map;
        }
        else
        {
            mapManager.Map = null;
        }
    }
}
