using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Mirror;
using System;
using UnityEditor;

public class Gun : MonoBehaviour
{
    public playerObjectController player;
    public Holdable holdable;

    public InputMaster controls;
    public LayerMask playerLayer;
    public LineRenderer laserBulletLine;

    public int ammo;

    public Transform firePoint;

    public Transform RightHandGrip { get; set; }

    public Transform LeftHandGrip { get; set; }



    public virtual void OnHold()
    {
        Debug.Log("Player is now holding a gun");
    }


    public void UpdateHoldable()
    {
        RightHandGrip = transform.GetChild(0).Find("Right_Hand_Grip");
        LeftHandGrip = transform.GetChild(0).Find("Left_Hand_Grip");

        if (RightHandGrip && LeftHandGrip == null) { return; }

        GetComponent<MeshFilter>().sharedMesh = holdable.itemMesh;

        LeftHandGrip.localPosition = holdable.LeftHandGripPos;
        RightHandGrip.localPosition = holdable.RightHandGripPos;
        LeftHandGrip.localRotation = Quaternion.Euler(holdable.LeftHandGripRot);
        RightHandGrip.localRotation = Quaternion.Euler(holdable.RightHandGripRot);

        UpdateHoldable();

        Debug.Log(LeftHandGrip + " | " + RightHandGrip);
        Debug.Log(holdable.LeftHandGripPos + " | " + holdable.RightHandGripPos);
    }

    public IEnumerator BombAttack()
    {
        player.GetComponent<Animator>().SetTrigger("BombAttack");
        player.leftHandRig.weight = 0;

        Physics.CheckSphere(player.rightHandRig.transform.position, .5f, playerLayer);

        

        yield return new WaitForSeconds(.3f);



        LeanTween.value(gameObject, 0, 1, .2f).setOnUpdate((float val) => { player.leftHandRig.weight = val; });
        player.rightHandRig.weight = 1;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(player.rightHandRig.transform.position, .5f);
    }

    public IEnumerator Attack()
    {
        if(holdable != null)
        {
            if (holdable.type == Holdable.Type.Gun)
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 999f, playerLayer))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    Debug.Log("Did Hit");

                    LineRenderer laserBulletLineInstance = Instantiate(laserBulletLine);

                    laserBulletLineInstance.SetPosition(0, firePoint.position);
                    laserBulletLineInstance.SetPosition(1, hit.point);

                    NetworkServer.Spawn(laserBulletLineInstance.gameObject);

                    AudioManager.instance.Play("LaserBullet");

                    if (hit.collider.TryGetComponent<TestDissolve>(out TestDissolve testDissolve) && hit.collider.gameObject.GetComponent<NetworkIdentity>().hasAuthority == false)
                    {
                        testDissolve.CmdDie();
                    }

                    yield return new WaitForSeconds(0.3f);

                    Destroy(laserBulletLineInstance.gameObject);
                }
            }
            else if (holdable.type == Holdable.Type.Bomb)
            {
                StartCoroutine(BombAttack());
            }
        }
    }
}