using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player3DView : MonoBehaviour
{
    public InputMaster controls;

    public float roateSens;

    // Start is called before the first frame update
    void Start()
    {
        controls = new InputMaster();
        controls.Enable();
    }

    private void RotatePlayer(float dir)
    {
        if (Mouse.current.wasUpdatedThisFrame)
        {
            if (!Mouse.current.leftButton.isPressed) { return; }
        }

        transform.Rotate(0, transform.rotation.y + dir * roateSens, 0);
        Debug.Log(dir);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
