using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public Transform inventoryItem;

    SteamInventoryResult_t _result;

    public static readonly int maxInventorySize = 100;

    protected Callback<SteamInventoryResultReady_t> steamInventoryResultReady;
    protected Callback<SteamInventoryRequestPricesResult_t> steamInventoryPriceResult;
    protected Callback<SteamInventoryStartPurchaseResult_t> startPuchaseResult;

    public bool steamPurchase;

    public SteamItemDetails_t[] m_SteamItemDetails;
    public SteamItemDef_t[] inventoryItems = new SteamItemDef_t[maxInventorySize];

    public uint inventorySize;

    public Transform inventoryRect;
    private Transform content;



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

            _result = SteamInventoryResult_t.Invalid;
            m_SteamItemDetails = null;

            SteamInventory.GetAllItems(out _result);
            SteamInventory.LoadItemDefinitions();
            SteamInventory.RequestPrices();
        }
    }

    private void OnEnable()
    {
        GetItems();
    }
    
    public void GetItems()
    {
        content = inventoryRect.Find("Content");
        if (SteamManager.Initialized)
        {
            SteamAPI.RunCallbacks();
            SteamUserStats.StoreStats();

            _result = SteamInventoryResult_t.Invalid;
            m_SteamItemDetails = null;

            SteamInventory.GetAllItems(out _result);
            SteamInventory.LoadItemDefinitions();
            SteamInventory.RequestPrices();
        }
        else
        {
            Debug.LogError("SteamManager Not Initialized");
        }
    }

    private void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            GetItems();
        }
    }

    private void OnSteamInventoryResultReady(SteamInventoryResultReady_t result)
    {
        _result = result.m_handle;
        Debug.Log(SteamInventory.GetResultStatus(_result));
        Debug.Log(m_SteamItemDetails);

        if (m_SteamItemDetails == null)
        {
            bool ret = SteamInventory.GetResultItems(_result, null, ref inventorySize);
            if (ret && inventorySize > 0)
            {
                m_SteamItemDetails = new SteamItemDetails_t[inventorySize];
                ret = SteamInventory.GetResultItems(_result, m_SteamItemDetails, ref inventorySize);
            }
            else
            {
                Debug.Log($"<color=red>Error: Failed to load invenotory. Ret: {ret} Array-size: {inventorySize}");
            }
        }
        SteamInventory.DestroyResult(_result);
    }


    public void RemoveAllItems()
    {
        m_SteamItemDetails = null;
        SteamInventory.GetAllItems(out _result);

        StartCoroutine(RemoveAllItemsCou());
    }

    private IEnumerator RemoveAllItemsCou()
    {
        yield return new WaitForSeconds(2f);
        if (m_SteamItemDetails != null)
        {
            for (int i = 0; i < m_SteamItemDetails.Length; i++)
            {
                yield return new WaitForSeconds(.1f);
                inventoryItems[i].m_SteamItemDef = 0;
                Debug.Log(inventoryItems[i].m_SteamItemDef);
                SteamInventory.ConsumeItem(out _result, m_SteamItemDetails[i].m_itemId, 1);
            }
        }
        else
        {
            yield return null;
        }
    }

    public IEnumerator UpdateSteamInventoryItems()
    {
        m_SteamItemDetails = null;
        SteamInventory.GetAllItems(out _result);

        yield return new WaitForSeconds(.3f);

        //Remove all InventoryItems
        if (content.childCount > 0)
        {
            foreach (Transform item in content)
            {
                Destroy(item.gameObject);
            }
        }

        //Create the new inventory-items
        for (int i = 0; i < inventorySize; i++)
        {
            yield return new WaitForSeconds(.025f);

            if(m_SteamItemDetails[i].m_iDefinition.m_SteamItemDef != 0) {
                var inventoryItemInstance = Instantiate(inventoryItem, content);

                TMP_Text inventoryItemNameText = inventoryItemInstance.Find("Item Name").GetComponent<TMP_Text>();
                RawImage inventoryItemIcon = inventoryItemInstance.Find("Icon").GetComponent<RawImage>();

                string itemName = string.Empty;
                string itemIconUrl = string.Empty;
                uint bufferItemName = 2048;
                uint bufferItemUrl = 2048;

                inventoryItems[i] = m_SteamItemDetails[i].m_iDefinition;

                SteamInventory.GetItemDefinitionProperty(inventoryItems[i], "name", out itemName, ref bufferItemName);
                SteamInventory.GetItemDefinitionProperty(inventoryItems[i], "icon_url", out itemIconUrl, ref bufferItemUrl);
                Debug.Log(itemName + " | " + itemIconUrl);

                inventoryItemInstance.name = itemName + " Index " + i;

                inventoryItemNameText.text = itemName;
                UnityWebRequest www = UnityWebRequestTexture.GetTexture(itemIconUrl);
                yield return www.SendWebRequest();

                Texture myTexture = DownloadHandlerTexture.GetContent(www);
                inventoryItemIcon.texture = myTexture;
            }
        }
    }
}
