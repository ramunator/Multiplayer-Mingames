using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource SFX;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlaySFX()
    {
        SFX.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer") || other.CompareTag("Player"))
        {
            anim.SetTrigger("OpenDoor");
        }
    }

}
