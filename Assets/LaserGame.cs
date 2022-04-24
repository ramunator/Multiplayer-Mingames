using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Animations.Rigging;

public class LaserGame : NetworkBehaviour
{
    public Gun laserGun;

    private MyNetworkManager networkManager;

    private MyNetworkManager NetworkManager
    {
        get
        {
            if (networkManager != null)
            {
                return networkManager;
            }
            return networkManager = MyNetworkManager.singleton as MyNetworkManager;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
        {
            GiveAllPlayersLaserGun();
        }
    }

        public void GiveAllPlayersLaserGun()
        {
            foreach(playerObjectController player in NetworkManager.GamePlayers)
            {
                player.rig.gameObject.SetActive(true);
                player.GetComponent<NetworkPlayerController>().gun = player.rig.GetChild(0).GetComponent<Gun>();
            }
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
