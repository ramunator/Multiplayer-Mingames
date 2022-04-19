using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyCharacterMenu : MonoBehaviour
{
    public static BuyCharacterMenu Instance { get; private set; }

    public PlayerSO playerSo;
    public CharacterSlot characterSlot;

    private void Awake()
    {
        Instance = this;
    }

    public void BuyPlayerButton()
    {
        PlayerPrefs.SetInt("Cash", PlayerPrefs.GetInt("Cash") - characterSlot.ammount);
        SetBought();
        transform.gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.GetChild(1).GetComponent<TMP_Text>().text = $"Do you want to buy {playerSo.name}";
    }

    public static bool SetBought()
    {
        //PlayerPrefs.SetInt($"Instance.characterSlot.playerId")
        return Instance.characterSlot.isBought = true;
    }
}
