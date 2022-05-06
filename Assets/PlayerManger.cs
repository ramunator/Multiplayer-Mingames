using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManger : MonoBehaviour
{
    PlayerController player;
    public static int playerMatID;
    public static int PlayerId;

    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<PlayerController>())
        {
            player = FindObjectOfType<PlayerController>();
        }
        
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
