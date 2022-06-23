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

    protected Callback<SteamInventoryResultReady_t> steamInventoryResultReady;
    protected Callback<SteamInventoryRequestPricesResult_t> steamInventoryPriceResult;

    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;

    public SteamItemDetails_t[] steamItems;
    public SteamItemDef_t[] itemDef;

    public uint itemIdsSize = 0;
    public uint itemSizeTest = 0;

    void Awake()
    {
        SteamAPI.Init();
    }

    private void Start()
    {
        if (SteamManager.Initialized)
        {
            steamInventoryResultReady = Callback<SteamInventoryResultReady_t>.Create(OnSteamInventoryResultReady);
            steamInventoryPriceResult = Callback<SteamInventoryRequestPricesResult_t>.Create(OnSteamInventoryPriceResult);
        }
            

    }

    private void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {

            SteamInventory.GetAllItems(out _result);

            SteamInventory.GetItemDefinitionIDs(itemDef, ref itemIdsSize);

            Debug.Log(itemIdsSize);

            SteamUserStats.SetAchievement("Test");

            SteamInventory.RequestPrices();


            text2.text = ("Items = " + _result);
            //SteamInventory.GetItemsWithPrices(itemDef, out currentPrice, out basePrice);

            SteamInventory.GetResultStatus(_result);

            Debug.Log(itemDef + " | " + itemDef.Length);
        }
    }

    private void OnSteamInventoryResultReady(SteamInventoryResultReady_t result)
    {
        SteamInventory.AddPromoItem(out result.m_handle, (SteamItemDef_t)100);

        SteamInventory.GetResultItems(result.m_handle, steamItems, ref itemSizeTest);

        Debug.Log(steamItems);

        text1.text = ("We Got an result " + steamItems[0] + " | " + currentPrice);
    }

    private void OnSteamInventoryPriceResult(SteamInventoryRequestPricesResult_t result)
    {
        text3.text = $"Price {result.m_result}";

        Debug.Log($"Price {result.m_result}");
    }
}