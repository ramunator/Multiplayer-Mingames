using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIToggle : MonoBehaviour
{
    public Transform UI;

    public bool uiActive = true;

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.ctrlKey.isPressed && Keyboard.current.uKey.wasPressedThisFrame && uiActive)
        {
            UI.gameObject.SetActive(false);
            uiActive = false;
        }
        else if(Keyboard.current.ctrlKey.isPressed && Keyboard.current.uKey.wasPressedThisFrame && !uiActive)
        {
            UI.gameObject.SetActive(true);
            uiActive = true;
        }
    }
}
