using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Gameplay/Player")]
public class PlayerSO : ScriptableObject
{
    public new string name;
    public int age;
    public string desc;
    public Sprite icon;
    public Mesh playerObjectPf;
}
