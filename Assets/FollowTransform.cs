using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FollowTransform : MonoBehaviour
{
    public Transform target;

    public enum FollowMode{
        putOnTopOf,
        Multiply,
        Subtract,
        Set
    };

    public FollowMode followMode;

    [Range(0f, 1f)]
    public float lerpTest;

    public bool[] FollowXYZPos = new bool[3];
    public bool[] FollowXYZRot = new bool[3];

    public float[] TargetPos = new float[3];
    public float[] TargetRot = new float[3];

    private float[] Pos = new float[3];
    private float[] Rot = new float[3];

    bool changeOffset;

    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        TargetPos[0] = target.localPosition.x;
        TargetPos[1] = target.localPosition.y;
        TargetPos[2] = target.localPosition.z;
        TargetRot[0] = target.localRotation.eulerAngles.x;
        TargetRot[1] = target.localRotation.eulerAngles.y;
        TargetRot[2] = target.localRotation.eulerAngles.z;

        if (followMode == FollowMode.Set)
        {
            for (int i = 0; i < FollowXYZPos.Length; i++)
            {
                if(FollowXYZPos[i] == false) { TargetPos[i] = transform.position[i]; continue; }

                Pos[i] = TargetPos[i];
            }

            for (int i = 0; i < FollowXYZRot.Length; i++)
            {
                if (FollowXYZRot[i] == false) { TargetRot[i] = transform.rotation[i]; continue; }

                Rot[i] = TargetRot[i];
            }

            gameObject.transform.rotation = Quaternion.Euler(Rot[0], Rot[1], Rot[2]);

            gameObject.transform.position = new Vector3(Pos[0] + offset.x, Pos[1] + offset.y, Pos[2] + offset.z);

            if (changeOffset)
            {
                lerpTest += Time.deltaTime;
                offset = Vector3.Lerp(gameObject.transform.localPosition, new Vector3(-0.45f, 0.09f, -0.895f), lerpTest);
            }
            if(lerpTest >= 1)
            {
                changeOffset = false;
            }
        }
    }

    public void WaitToSet2ndCamPos()
    {
        changeOffset = true;
    }

    public void RemoveOffset()
    {
        changeOffset = false;
        offset = Vector3.zero;
    }
}
