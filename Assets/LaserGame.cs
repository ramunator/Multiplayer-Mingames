using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Animations.Rigging;

public class LaserGame : NetworkBehaviour
{
    public Gun laserGun;

    public Holdable laserGunHoldable;

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
        foreach (playerObjectController player in NetworkManager.GamePlayers)
        {
            player.GetComponent<NetworkPlayerController>().currentHoldableItem = laserGunHoldable;

            player.gun.holdable = laserGunHoldable;

            player.leftHandRig.weight = 1;
            player.rightHandRig.weight = 1;

            player.gun.UpdateHoldable();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
