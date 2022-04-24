using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Mirror;

public class Gun : MonoBehaviour
{
    public InputMaster controls;
    public LayerMask playerLayer;
    public LineRenderer laserBulletLine;

    public Transform firePoint;
    public Transform rightGrip;
    public Transform leftGrip;

    public IEnumerator Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 999f, playerLayer)) 
        {  
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");

            if(hit.collider.TryGetComponent<TestDissolve>(out TestDissolve testDissolve) && hit.collider.gameObject.GetComponent<NetworkIdentity>().hasAuthority == false)
            {
                LineRenderer laserBulletLineInstance = Instantiate(laserBulletLine);

                laserBulletLineInstance.SetPosition(0, firePoint.position);
                laserBulletLineInstance.SetPosition(1, hit.point);

                AudioManager.instance.Play("LaserBullet");

                testDissolve.Die();

                yield return new WaitForSeconds(0.5f);

                Destroy(laserBulletLineInstance);
            }

            Debug.Log(testDissolve);
        }
    }
}
