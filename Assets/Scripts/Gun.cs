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
    public int currentClip, maxClipSize = 6, currentAmmo, maxAmmoSize = 86;
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
    
    void Shoot()
    {
        if (currentClip > 0){ 
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
            
            Vector3 direction = (targetPoint - shootingPoint.position).normalized;
           

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

            currentClip--;
        }
    }

    public void Reload()
    {
        int reloadAmount = maxClipSize - currentClip;
        reloadAmount = (currentAmmo + reloadAmount) >= 0 ? reloadAmount : currentAmmo;
        currentClip += reloadAmount;
        currentAmmo -= reloadAmount;
    }

    public void AddAmmo(int ammoAmount)
    {
        currentAmmo += ammoAmount;
        if (currentAmmo > maxAmmoSize)
        {
            currentAmmo = maxAmmoSize;
        }
        
    }
   
}
