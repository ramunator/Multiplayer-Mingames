using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Customer", menuName ="Gameplay/Customer")]
public class Customer : ScriptableObject
{
    public enum Gender
    {
        Boy,
        Girl,
        Women,
        Man
    };

    public enum MoneyState
    {
        Poor,
        Normal,
        Rich
    };

    public string customerName = "No Customer Name";
    public GameObject pfModel;
    public MoneyState moneyState = MoneyState.Normal;
    public Gender gender;
    public Sprite icon;

}
