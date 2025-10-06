using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float bulletSpeed = 20f;
    public Camera playerCam;
    public ObjectPool bulletPool;
    public GameObject shootingPoint;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    [Obsolete("Obsolete")]
    void Shoot()
    {
        GameObject bullet  = bulletPool.GetObject();
        bullet.transform.position = shootingPoint.transform.position;
        bullet.transform.rotation = shootingPoint.transform.rotation;
        
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
        }

        StartCoroutine(DeactivateBullet(bullet));

        /*RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }*/

    }

    IEnumerator DeactivateBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(2f);
        bulletPool.ReturnObject(bullet);
    }
}
