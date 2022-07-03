using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using UnityEngine.InputSystem;
using TMPro;
using System.Text;

public class SteamIntergration : MonoBehaviour
{
    SteamInventoryResult_t _result;

    protected Callback<SteamInventoryResultReady_t> steamInventoryResultReady;
    protected Callback<SteamInventoryRequestPricesResult_t> steamInventoryPriceResult;
    protected Callback<RemoteStorageFileReadAsyncComplete_t> remoteStorageFileReadAsyncComplete;
    protected Callback<SteamInventoryStartPurchaseResult_t> startPuchaseResult;

    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;

    public bool steamPurchase;
    public bool useSteamCloud;

    private SteamItemDetails_t[] m_SteamItemDetails;
    public SteamItemDef_t[] itemDef;

    public SteamItemDetails_t testdaw;  

    public uint[] steamPurchaseItemAmmount;
    public uint[] steamGetItemsAmmount;

    SteamAPICall_t steamAPICall;

    public string testSteamGet;
    uint bufferSize = 100;

    public uint arraySize;


    void Awake()
    {
        SteamAPI.Init();
        SteamAPI.RestartAppIfNecessary(new AppId_t(2063270));
    }

    private void Start()
    {
        if (SteamManager.Initialized)
        {
            SteamAPI.RunCallbacks();
            SteamUserStats.StoreStats();

            steamInventoryResultReady = Callback<SteamInventoryResultReady_t>.Create(OnSteamInventoryResultReady);
            steamInventoryPriceResult = Callback<SteamInventoryRequestPricesResult_t>.Create(OnSteamInventoryPriceResult);
            remoteStorageFileReadAsyncComplete = Callback<RemoteStorageFileReadAsyncComplete_t>.Create(OnFileReadAsync);

            _result = SteamInventoryResult_t.Invalid;
            m_SteamItemDetails = null;
        }
            

    }

    private void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {

            SteamInventory.GetAllItems(out _result);

            SteamUserStats.SetAchievement("Test");

            if (useSteamCloud)
            {
                byte[] bytes = Encoding.ASCII.GetBytes("Test Test Haps");

                Debug.Log(bytes.Length);

                uint steamId = SteamUser.GetSteamID().GetAccountID().m_AccountID;

                bool test = SteamRemoteStorage.FileWrite(steamId + " test.txt", bytes, bytes.Length);

                int testString = SteamRemoteStorage.FileRead(steamId + " test.txt", bytes, bytes.Length);

                SteamRemoteStorage.FileReadAsyncComplete(steamAPICall, bytes, (uint)bytes.Length);

                bool exists = SteamRemoteStorage.FileExists(steamId + " test.txt");

                SteamRemoteStorage.FileReadAsync(steamId + " test.txt", 0, (uint)bytes.Length);

                Debug.Log(test + " | " + exists);
            }
        }
        if (Keyboard.current.backspaceKey.wasPressedThisFrame)
        {

            uint OutItemsArraySize = 0;
            bool ret = SteamInventory.GetResultItems(_result, null, ref OutItemsArraySize);
            if (ret && OutItemsArraySize > 0)
            {
                m_SteamItemDetails = new SteamItemDetails_t[OutItemsArraySize];
                ret = SteamInventory.GetResultItems(_result, m_SteamItemDetails, ref OutItemsArraySize);
                Debug.Log(SteamInventory.GetResultItems(_result, m_SteamItemDetails, ref OutItemsArraySize));

            }

            /*SteamInventory.GenerateItems(out _result, itemDef, steamGetItemsAmmount, (uint)itemDef.Length);
            SteamInventory.ConsumeItem(out _result, m_SteamItemDetails[0].m_itemId, 50);

            Debug.Log(SteamInventory.GetItemDefinitionProperty((SteamItemDef_t)250, "description", out testSteamGet, ref bufferSize));
            Debug.Log(testSteamGet + " | " + bufferSize);
            */
        }
        if (Keyboard.current.rightAltKey.wasPressedThisFrame)
        {
            for (int i = 0; i < m_SteamItemDetails.Length; i++)
            {
                SteamInventory.ConsumeItem(out _result, m_SteamItemDetails[i].m_itemId, 1);
            }
        }
    }

    private void OnFileReadAsync(RemoteStorageFileReadAsyncComplete_t remoteStorageFile)
    {
        Debug.Log("How far");

        steamAPICall = remoteStorageFile.m_hFileReadAsync;
        byte[] bytes = Encoding.ASCII.GetBytes("Test Test Haps");
        SteamRemoteStorage.FileReadAsyncComplete(steamAPICall, bytes, remoteStorageFile.m_cubRead);
        Debug.Log(steamAPICall.m_SteamAPICall);
        Debug.Log(steamAPICall);
        Debug.Log(remoteStorageFile.m_eResult);
        Debug.Log(remoteStorageFile.m_cubRead);

    }

    private void OnSteamInventoryResultReady(SteamInventoryResultReady_t result)
    {
        _result = result.m_handle;
        Debug.Log(SteamInventory.GetResultStatus(_result));

        //SteamInventory.TriggerItemDrop(out _result, itemDef[1]);

        SetSteamItemsAmmount(2, 1);

        SteamInventory.RequestPrices();

        ResetSteamItemsAmmount();

        SteamInventory.DestroyResult(_result);
    }

    public void BuyItem(int id)
    {
        if (steamPurchase)
        {
            ResetSteamPurchaseAmmount();
            steamPurchaseItemAmmount[id] = 1;
            if (itemDef.Length == steamPurchaseItemAmmount.Length)
            {
                SteamInventory.StartPurchase(itemDef, steamPurchaseItemAmmount, (uint)itemDef.Length);
            }
            else
            {
                Debug.LogError("SteamInventory Purchase Failed! ItemDef " + itemDef.Length + " is not the same as steampurchaselistammount " + steamPurchaseItemAmmount.Length);
            }
        }
    }

    private void ResetSteamItemsAmmount()
    {
        for (int i = 0; i < steamGetItemsAmmount.Length; i++)
        {
            steamGetItemsAmmount[i] = 0;
        }
    }


    private void SetSteamItemsAmmount(int id, uint number)
    {
        steamGetItemsAmmount[id] = number;
    }

    private void ResetSteamPurchaseAmmount()
    {
        for (int i = 0; i < steamPurchaseItemAmmount.Length; i++)
        {
            steamPurchaseItemAmmount[i] = 0;
        }
    }

    private void OnSteamInventoryPriceResult(SteamInventoryRequestPricesResult_t result)
    {
        text3.text = $"Price {result.m_result}";

        Debug.Log($"Price {result.m_result}");
    }
}