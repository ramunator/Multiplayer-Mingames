using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnTouch : MonoBehaviour
{
    public LayerMask playerLayer;

    [SerializeField] private float timeBeforeCanKill = 0.25f;

    float timeSpend = 0f;

    [SerializeField] private bool canBeKilled = false;

    private void Update()
    {
        if (canBeKilled) { return; }

        timeSpend += Time.deltaTime;

        if(timeSpend > timeBeforeCanKill)
        {
            canBeKilled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && canBeKilled)
        {
            Debug.Log(other);
            other.TryGetComponent(out NetworkPlayerController networkPlayer);
            StartCoroutine(networkPlayer.Die());
        }
    }
}
