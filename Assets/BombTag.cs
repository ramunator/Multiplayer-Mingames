using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTag : NetworkBehaviour
{
    public playerObjectController playerWithBomb;

    public float playerWithBombTime = 0;

    public Bomb bomb;

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
            GetNewPlayerTheBomb();
        }
    }

    public void GetNewPlayerTheBomb()
    {
        playerWithBomb = NetworkManager.GamePlayers[Random.Range(0, NetworkManager.GamePlayers.Count)];
        Debug.Log(playerWithBomb);
    }

    // Update is called once per frame
    void Update()
    {
        playerWithBombTime += Time.deltaTime;

        if(playerWithBombTime > 15)
        {
            Debug.Log($"BOOOMM {playerWithBomb.playerName} IS DEAD");
            playerWithBombTime = 0;
            bomb.Explode();
            GetNewPlayerTheBomb();
        }
    }
}
