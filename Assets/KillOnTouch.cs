using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnTouch : MonoBehaviour
{
    public LayerMask playerLayer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log(other);
            other.TryGetComponent(out NetworkPlayerController networkPlayer);
            StartCoroutine(networkPlayer.Die());
        }
    }
}
