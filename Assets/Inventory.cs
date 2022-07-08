using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Inventory : MonoBehaviour
{
    public Transform inventoryItem;

    SteamInventoryResult_t _result;

    protected Callback<SteamInventoryResultReady_t> steamInventoryResultReady;
    protected Callback<SteamInventoryRequestPricesResult_t> steamInventoryPriceResult;
    protected Callback<SteamInventoryStartPurchaseResult_t> startPuchaseResult;

    public bool steamPurchase;

    public SteamItemDetails_t[] m_SteamItemDetails;
    public SteamItemDef_t[] itemDefItem;

    public uint arraySize;

    private Transform inventoryRect;
    private Transform content;



    void Awake()
    {
        SteamAPI.Init();
        SteamAPI.RestartAppIfNecessary(new AppId_t(2063270));
    }

    private void Start()
    {
        inventoryRect = transform.Find("Inventory Rect");
        content = inventoryRect.Find("Content");

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

            StartCoroutine(UpdateSteamInventoryItems());
        }
    }
    private void OnSteamInventoryResultReady(SteamInventoryResultReady_t result)
    {
        _result = result.m_handle;
        Debug.Log(SteamInventory.GetResultStatus(_result));

        if (m_SteamItemDetails == null)
        {
            bool ret = SteamInventory.GetResultItems(_result, null, ref arraySize);
            if (ret && arraySize > 0)
            {
                m_SteamItemDetails = new SteamItemDetails_t[arraySize];
                ret = SteamInventory.GetResultItems(_result, m_SteamItemDetails, ref arraySize);
            }
            else
            {
                Console.Instance.AnswerCommand($"<color=red>Error: Failed to load invenotory. Ret: {ret} Array-size: {arraySize}");
            }
        }
        SteamInventory.DestroyResult(_result);
    }



    public IEnumerator UpdateSteamInventoryItems()
    {
        yield return new WaitForSeconds(2f);

        //Remove all InventoryItems
        if (content.childCount > 0)
        {
            foreach (Transform item in content)
            {
                Destroy(item);
            }
        }

        //Create the new inventory-items
        for (int i = 0; i < arraySize; i++)
        {
            yield return new WaitForSeconds(.5f);
            var inventoryItemInstance = Instantiate(inventoryItem, content);

            TMP_Text inventoryItemNameText = inventoryItemInstance.Find("Item Name").GetComponent<TMP_Text>();
            RawImage inventoryItemIcon = inventoryItemInstance.Find("Icon").GetComponent<RawImage>();

            string itemName = string.Empty;
            string itemIconUrl = string.Empty;
            uint bufferItemName = 2048;
            uint bufferItemUrl = 2048;
            foreach (SteamItemDetails_t itemDetails in m_SteamItemDetails)
            {
                Console.Instance.AnswerCommand(itemDetails.m_iDefinition + " | " + itemDefItem[i]);
                if(itemDetails.m_iDefinition == itemDefItem[i])
                {
                    SteamInventory.GetItemDefinitionProperty(itemDefItem[i], "name", out itemName, ref bufferItemName);
                    SteamInventory.GetItemDefinitionProperty(itemDefItem[i], "icon_url", out itemIconUrl, ref bufferItemUrl);
                    Debug.Log(itemName + " | " + itemIconUrl);
                }
            }

            inventoryItemNameText.text = itemName;
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(itemIconUrl);
            yield return www.SendWebRequest();

            Texture myTexture = DownloadHandlerTexture.GetContent(www);
            inventoryItemIcon.texture = myTexture;
        }
    }
}
