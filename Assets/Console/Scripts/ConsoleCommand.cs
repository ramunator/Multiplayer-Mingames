using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Gameplay/Console/Command")]
public class ConsoleCommand : ScriptableObject
{
    public string commandName;
    public int argsLenght;
}
