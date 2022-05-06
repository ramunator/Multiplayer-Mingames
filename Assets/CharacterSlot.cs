using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    public enum Payment
    {
        cash,
        free
    };
    public Payment payment;
    public int ammount;
    public SelectPlayer player;

    public bool isBought;

    public PlayerSO playerSo;
    public int playerId;

    // Start is called before the first frame update
    void Start()
    {
        if(payment == Payment.free) { isBought = true; }

        transform.Find("PlayerName").GetComponent<TMP_Text>().text = playerSo.name;
        transform.Find("Icon").GetComponent<Image>().sprite = playerSo.icon;

       
    }


    public void SelectPlayer()
    {
        if(isBought == false) { BuyPlayer(); return; }

        PlayerManger.PlayerId = playerId;
        player.playerMesh.sharedMesh = playerSo.playerObjectPf;

        player.currentPlayer = playerSo;

        SaveSystemJson.SavePlayerData();
    }

    public void BuyPlayer()
    {
        if(isBought != false) { return; }

        transform.parent.parent.parent.Find("BuyCharacter").gameObject.SetActive(true);

        FindObjectOfType<BuyCharacterMenu>().characterSlot = this;
        FindObjectOfType<BuyCharacterMenu>().playerSo = playerSo;
    }



    // Update is called once per frame
    void Update()
    {
        if (isBought)
        {
            transform.Find("Lock").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("Lock").gameObject.SetActive(true);
            transform.Find("Background").GetComponent<Image>().color = Color.grey;
        }
    }
}
