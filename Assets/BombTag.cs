using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTag : NetworkBehaviour
{
    public GameObject explosionParticle;

    public bool killPlayer;

    public playerObjectController playerWithBomb;

    public float playerWithBombTime = 0;

    public Holdable bombHoldable;

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
        foreach(playerObjectController playerObjectController in NetworkManager.GamePlayers)
        {
            playerObjectController.leftHandRig.weight = 0;
            playerObjectController.rightHandRig.weight = 0;

            playerObjectController.gun.GetComponent<MeshFilter>().mesh = null;
        }

        playerObjectController player = NetworkManager.GamePlayers[Random.Range(0, NetworkManager.GamePlayers.Count)];

        playerWithBomb = player;

        player.GetComponent<NetworkPlayerController>().currentHoldableItem = bombHoldable;
        player.gun.holdable = bombHoldable;

        player.leftHandRig.weight = 1;
        player.rightHandRig.weight = 1;

        player.gun.UpdateHoldable();

        player.gun.GetComponent<MeshFilter>().mesh = bombHoldable.itemMesh;
    }

    // Update is called once per frame
    void Update()
    {
        playerWithBombTime += Time.deltaTime;

        if(playerWithBombTime > 22.5f)
        {
            KillPlayerWithBomb();
        }
    }

    private void KillPlayerWithBomb()
    {
        Instantiate(explosionParticle, playerWithBomb.transform.position, Quaternion.identity);

        playerWithBombTime = 0;

        ChatBehaiuver.Instance.Send($"<color=white>BOOOMM <color=red>[{playerWithBomb.playerName}] IS DEAD", ChatBehaiuver.MessageTypeEnum.ServerMessage);

        if (killPlayer)
        {
            playerWithBomb.gameObject.SetActive(false);
        }

        GetNewPlayerTheBomb();
    }
}
