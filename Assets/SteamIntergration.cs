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

    public bool steamPurchase;
    public bool useSteamCloud;

    public SteamItemDetails_t[] m_SteamItemDetails;
    public SteamItemDef_t[] itemDefItem;
    public SteamItemDef_t[] itemDefDrop;

    public SteamItemDetails_t testdaw;  

    public uint[] steamPurchaseItemAmmount;
    public uint[] steamGetItemsAmmount;

    SteamAPICall_t steamAPICall;

    public string testSteamGet;
    uint bufferSize = 100;

    public uint arraySize;
    public uint generateItemsArray;


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

            SteamInventory.GetAllItems(out _result);
            SteamInventory.LoadItemDefinitions();
            SteamInventory.RequestPrices();
        }
    }

    private void Update()
    {
        /*if (Keyboard.current.enterKey.wasPressedThisFrame)
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

            }*/

        /*SteamInventory.GenerateItems(out _result, itemDef, steamGetItemsAmmount, (uint)itemDef.Length);
        SteamInventory.ConsumeItem(out _result, m_SteamItemDetails[0].m_itemId, 50);

        Debug.Log(SteamInventory.GetItemDefinitionProperty((SteamItemDef_t)250, "description", out testSteamGet, ref bufferSize));
        Debug.Log(testSteamGet + " | " + bufferSize);

    }*/
    }

    public void GetAllItems()
    {
        m_SteamItemDetails = null;
        SteamInventory.GetAllItems(out _result);

    }



    public void RemoveItem(SteamItemDef_t itemDefId)
    {
        if (m_SteamItemDetails != null)
        {
            for (int i = 0; i < m_SteamItemDetails.Length; i++)
            {
                if (m_SteamItemDetails[i].m_iDefinition == itemDefId)
                {
                    bool ret = SteamInventory.ConsumeItem(out _result, m_SteamItemDetails[i].m_itemId, 1);
                    print("SteamInventory.ConsumeItem(out m_SteamInventoryResult, " + m_SteamItemDetails[0].m_itemId + ", 1) - " + ret + " -- " + _result);
                }
            }
        }
    }

    public void TriggerRandomItem()
    {
        int index = UnityEngine.Random.Range(0, itemDefDrop.Length);
        SteamInventory.GetAllItems(out _result);

        Console.Instance.AnswerCommand($"<color=green>{SteamFriends.GetPersonaName()} Got an {itemDefDrop[index]}");
    }

    public void GetItem(SteamItemDef_t itemDefId)
    {
        ResetSteamItemsAmmount();
        uint generateItemArray = (uint)itemDefItem.Length;
        for (int i = 0; i < itemDefItem.Length; i++)
        {
            if(itemDefItem[i] == itemDefId)
            {
                SetSteamItemsAmmount(i, 1);
                SteamInventory.GenerateItems(out _result, itemDefItem, steamGetItemsAmmount, generateItemArray);

                string itemName;
                uint bufferItemName = 2048;
                SteamInventory.GetItemDefinitionProperty(itemDefItem[1], "name", out itemName, ref bufferItemName);

                Console.Instance.AnswerCommand($"<color=green>{SteamFriends.GetPersonaName()} got an {itemName}!");
                return;
            }
            else
            {
                Console.Instance.AnswerCommand($"<color=red>There was no item with the id: {itemDefId}!");
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

        if(m_SteamItemDetails == null)
        {
            bool ret = SteamInventory.GetResultItems(_result, null, ref arraySize);
            if (ret && arraySize > 0)
            {
                m_SteamItemDetails = new SteamItemDetails_t[arraySize];
                ret = SteamInventory.GetResultItems(_result, m_SteamItemDetails, ref arraySize);
            }
            else
            {
                Console.Instance.AnswerCommand($"<color=red>Error: Ret: {ret} Array-size: {arraySize}");
            }
        }
        //SteamInventory.TriggerItemDrop(out _result, itemDef[1]);
        SteamInventory.RequestPrices();

        SteamInventory.DestroyResult(_result);
    }

    public void BuyItem(int id)
    {
        if (steamPurchase)
        {
            ResetSteamPurchaseAmmount();
            steamPurchaseItemAmmount[id] = 1;
            if (itemDefItem.Length == steamPurchaseItemAmmount.Length)
            {
                SteamInventory.StartPurchase(itemDefItem, steamPurchaseItemAmmount, (uint)itemDefItem.Length);
            }
            else
            {
                Debug.LogError("SteamInventory Purchase Failed! ItemDef " + itemDefItem.Length + " is not the same as steampurchaselistammount " + steamPurchaseItemAmmount.Length);
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


    public void SetSteamItemsAmmount(int id, uint number)
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
        Debug.Log($"Price {result.m_result}");
    }
}