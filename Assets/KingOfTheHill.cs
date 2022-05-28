using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KingOfTheHill : NetworkBehaviour
{
    public List<float> playerScores = new List<float>();

    public float scoreMultiplier = 4;

    public Holdable boxingGlove;

    private MyNetworkManager networkManager;

    public Transform kingOfTheHillScore;

    public LayerMask playerLayer;

    public Transform startPlace;
    public float size;

    private Transform scoreInstance;

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


    private void GiveAllPlayersBoxinGlove()
    {
        foreach (playerObjectController player in NetworkManager.GamePlayers)
        {
            player.GetComponent<NetworkPlayerController>().currentHoldableItem = boxingGlove;

            player.gun.holdable = boxingGlove;

            player.leftHandRig.weight = 0;
            player.rightHandRig.weight = 1;

            player.gun.UpdateHoldable();

            player.gun.GetComponent<MeshFilter>().mesh = boxingGlove.itemMesh;
        }
    }


    private void Start()
    {
        foreach(playerObjectController players in NetworkManager.GamePlayers)
        {
            playerScores.Add(0);
            scoreInstance = Instantiate(kingOfTheHillScore, players.gameObject.transform.Find("Canvas"));
        }
        GiveAllPlayersBoxinGlove();
    }

    void Update()
    {
        Collider[] players = Physics.OverlapSphere(startPlace.position, size, playerLayer);
        {
            foreach (Collider player in players)
            {
                for (int i = 0; i < NetworkManager.GamePlayers.Count; i++)
                {
                    if(NetworkManager.GamePlayers[i] == player.gameObject.GetComponent<playerObjectController>())
                    {
                        Debug.Log(NetworkManager.GamePlayers[i].playerName + " Is Hitting With The Hill");
                        playerScores[i] += Time.deltaTime * 5;
                        scoreInstance.GetChild(0).GetComponent<TextMeshProUGUI>().text = playerScores[i].ToString("F0");
                    }
                }
                Debug.Log(players.Length + " Are Currently In The Zone");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(startPlace.position, size);
    }
}
