using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public ParticleSystem explosion;

    public void Explode()
    {
        GetComponent<MeshRenderer>().enabled = false;
        explosion.Play();
    }

    
}
