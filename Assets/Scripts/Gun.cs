using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Gun : MonoBehaviour
{
    public float damage = 85f;
    public float range = 100f;
    public float bulletSpeed = 1500f;
    public Camera playerCam;
    public ObjectPool bulletPool;
    public Transform shootingPoint;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    //[Obsolete("Obsolete")]
    void Shoot()
    {
        Ray camRay = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 targetPoint;
        RaycastHit hit;
        if (Physics.Raycast(camRay, out hit, range))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = camRay.origin + camRay.direction * range;
        }
        
        GameObject bullet  = bulletPool.GetObject();
        bullet.transform.position = shootingPoint.position;
        
        //bullet.transform.rotation = shootingPoint.transform.rotation;
        Vector3 direction = (targetPoint - shootingPoint.position).normalized;
        /*Target target = hit.transform.GetComponent<Target>();
        if (target != null)
        {
            target.TakeDamage(damage);
        }*/
        

        if (Vector3.Distance(shootingPoint.position, targetPoint) < 0.1f)
        {
            direction = playerCam.transform.forward; 
        }
        
        bullet.transform.rotation = Quaternion.LookRotation(direction);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.velocity = bullet.transform.forward * bulletSpeed;
        }
        
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.pool = bulletPool;
            bulletScript.lifeTime = 5f;
            bulletScript.damage = this.damage;
        }

       // StartCoroutine(DeactivateBullet(bullet));

        /*RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }*/

    }

   /* IEnumerator DeactivateBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(2f);
        bulletPool.ReturnObject(bullet);
    }*/
}
