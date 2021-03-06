using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using TMPro;

public class LobbyListManager : MonoBehaviour
{
    public static LobbyListManager Instance;

    public GameObject lobbyDataItemPrefab;
    public GameObject lobbyListContent;

    public GameObject buttons;

    public TMP_InputField lobbyNameField;

    public ELobbyDistanceFilter elobbyDistanceFilter;

    public List<GameObject> LobbyList = new List<GameObject>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void GetDistanceFilter(int index)
    {
        Debug.Log(index);
        if(index == 0) { elobbyDistanceFilter = ELobbyDistanceFilter.k_ELobbyDistanceFilterDefault; }
        if(index == 1) { elobbyDistanceFilter = ELobbyDistanceFilter.k_ELobbyDistanceFilterClose; }
        if(index == 2) { elobbyDistanceFilter = ELobbyDistanceFilter.k_ELobbyDistanceFilterFar; }
    }

    public void GetListOfLobby()
    {
        buttons.gameObject.SetActive(false);

        SteamLobbyManager.Instance.GetLobbiesList(string.Empty, ELobbyDistanceFilter.k_ELobbyDistanceFilterDefault);
    }

    public void UpdateLobbiesList()
    {
        SteamLobbyManager.Instance.GetLobbiesList(lobbyNameField.text, elobbyDistanceFilter);
    }


    public void RefreshList()
    {
        lobbyNameField.text = string.Empty;
        SteamLobbyManager.Instance.GetLobbiesList(string.Empty, ELobbyDistanceFilter.k_ELobbyDistanceFilterDefault);
    }

    public void DisplayLobby(List<CSteamID> lobbyIds, LobbyDataUpdate_t result)
    {
        for (int i = 0; i < lobbyIds.Count; i++)
        {
            if(lobbyIds[i].m_SteamID == result.m_ulSteamIDLobby)
            {
                GameObject itemInstance = Instantiate(lobbyDataItemPrefab);

                itemInstance.GetComponent<LobbyDataEntry>().lobbyId = (CSteamID)lobbyIds[i].m_SteamID;
                itemInstance.GetComponent<LobbyDataEntry>().lobbyName = 
                    SteamMatchmaking.GetLobbyData((CSteamID)lobbyIds[i].m_SteamID, "name");

                itemInstance.GetComponent<LobbyDataEntry>().SetLobbyData();

                itemInstance.transform.SetParent(lobbyListContent.transform);
                itemInstance.transform.localScale = Vector3.one;

                LobbyList.Add(itemInstance);
            }
        }
    }

    public void DestroyLobby()
    {
        foreach(GameObject lobbyItem in LobbyList)
        {
            Destroy(lobbyItem);
        }
        LobbyList.Clear();
    }

}
