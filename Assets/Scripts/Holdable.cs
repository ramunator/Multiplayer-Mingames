using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Gameplay/Item")]
public class Holdable : ScriptableObject
{
    public enum Type
    {
        Gun,
        Bomb,
        Item,
        BoxingGlove
    };

    public Type type;

    public new string name;

    public Mesh itemMesh;

    public Vector3 RightHandGripRot;
    public Vector3 LeftHandGripRot;
    public Vector3 RightHandGripPos;
    public Vector3 LeftHandGripPos;
}