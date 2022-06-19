using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using UnityEngine.InputSystem;
using TMPro;

public class SteamIntergration : MonoBehaviour
{
    SteamInventoryResult_t _result;

    ulong currentPrice;
    ulong basePrice;

    protected Callback<SteamInventoryResultReady_t> steamInventoryResult;
    protected Callback<SteamInventoryRequestPricesResult_t> steamInventoryPriceResult;

    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;

    void Awake()
    {
        SteamAPI.Init();
    }

    private void Start()
    {
        if (SteamManager.Initialized)
        {
            steamInventoryResult = Callback<SteamInventoryResultReady_t>.Create(OnSteamInventoryResult);
            steamInventoryPriceResult = Callback<SteamInventoryRequestPricesResult_t>.Create(OnSteamInventoryPriceResult);
        }
            

    }

    private void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            SteamUserStats.SetAchievement("Test");

            SteamInventory.RequestPrices();

            text1.text = ("Test " + _result);

            SteamInventory.GetAllItems(out _result);

            text2.text = ("Items = " + _result);

            SteamInventory.GetItemPrice((SteamItemDef_t)100, out currentPrice, out basePrice);

            SteamInventory.GetResultStatus(_result);
        }
    }

    private void OnSteamInventoryResult(SteamInventoryResultReady_t result)
    {
        text1.text = ("We Got an result " + result.m_result + " | " + currentPrice);
        SteamInventory.DestroyResult(_result);
    }

    private void OnSteamInventoryPriceResult(SteamInventoryRequestPricesResult_t result)
    {
        text3.text = $"Price {result.m_result}";
        SteamInventory.DestroyResult(_result);
    }
}