using System;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    private EnemyAI parentEnemy;

    private void Awake()
    {
        parentEnemy = GetComponentInParent<EnemyAI>();
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            parentEnemy.TakeHeadshot();
        }
    }
}
