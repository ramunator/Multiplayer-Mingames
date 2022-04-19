using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public List<AudioSource> Sfx = new List<AudioSource>();
    
    public void PlaySFX(int id)
    {
        Sfx[id].Play();
    }

}
