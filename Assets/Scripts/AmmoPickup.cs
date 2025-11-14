using System;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Gun gun = collision.gameObject.GetComponentInChildren<Gun>();
        if (gun)
        {
            gun.AddAmmo(gun.maxClipSize * 3);
            Destroy(gameObject);
        }
    }
}
