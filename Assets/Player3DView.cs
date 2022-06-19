using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player3DView : MonoBehaviour
{
    public RotatePlayer controls;

    public float roateSens;

    // Start is called before the first frame update
    void Start()
    {
        controls = new RotatePlayer();
        controls.Enable();

        controls.Rotation.Rotate.started += ctx => RotatePlayer(ctx.ReadValue<float>());
        controls.Rotation.Rotate.performed += ctx => RotatePlayer(ctx.ReadValue<float>());
        controls.Rotation.Rotate.canceled += ctx => RotatePlayer(ctx.ReadValue<float>());
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void RotatePlayer(float dir)
    {
        if (Mouse.current.wasUpdatedThisFrame)
        {
            if (!Mouse.current.leftButton.isPressed) { return; }
        }

        transform.Rotate(0, transform.rotation.y + dir * roateSens * Time.deltaTime, 0);
        Debug.Log(dir);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
