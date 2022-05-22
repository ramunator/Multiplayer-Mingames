using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewCamera : MonoBehaviour
{
    public GameObject Preview;

    public float speed;

    // Start is called before the first fradme update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Preview.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}
