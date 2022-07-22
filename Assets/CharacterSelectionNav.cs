using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.Events;

public class CharacterSelectionNav : MonoBehaviour
{

    public NavInput navInput;

    public Inventory inventory;

    public ControllerUI controllerUI;

    public RawImage backIcon;
    public RawImage selectIcon;
    public RawImage refreshIcon;

    public UnityEvent backEvent;

    private void Awake()
    {
        navInput = new NavInput();

        navInput.CharacterSelection.Back.performed += ctx => Back();
        navInput.CharacterSelection.Refresh.performed += ctx => Refresh();
    }

    private void Start()
    {
        GetIcons();
        Refresh();
    }

    private void GetIcons()
    {
        SteamGlyphLoader.LoadSteamGlyph(EInputActionOrigin.k_EInputActionOrigin_SteamController_B, backIcon);
        SteamGlyphLoader.LoadSteamGlyph(EInputActionOrigin.k_EInputActionOrigin_SteamController_A, selectIcon);
        SteamGlyphLoader.LoadSteamGlyph(EInputActionOrigin.k_EInputActionOrigin_SteamController_Y, refreshIcon);
    }

    private void Refresh()
    {
        StartCoroutine(inventory.UpdateSteamInventoryItems());
        float waitTime = 0.25f;
        for (int i = 0; i < inventory.inventoryItems.Length; i++)
        {
            if(inventory.inventoryItems[i].m_SteamItemDef != 0)
            {
                waitTime += .025f;
            }
        }
        controllerUI.GetControllerUI(waitTime);
    }

    private void OnEnable()
    {
        navInput.Enable();
        GetIcons();
    }

    private void OnDisable()
    {
        navInput.Disable();
    }

    private void Back()
    {
        Debug.Log("Test");
        backEvent?.Invoke();
    }
}
