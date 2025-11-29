using System;
using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float lifeTime;
    public ObjectPool pool;
    public float damage;
    
    Rigidbody rb;
    Coroutine lifeCoroutine;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        lifeCoroutine = StartCoroutine(ReturnAfter(lifeTime));
        
    }

    void OnDisable()
    {
        if (lifeCoroutine != null)
        {
            StopCoroutine(lifeCoroutine);
            lifeCoroutine = null;
        }
    }

    IEnumerator ReturnAfter(float time)
    {
        yield return new WaitForSeconds(time);
        ReturnToPool();
    }

    void OnCollisionEnter(Collision collision)
    {
        Target target = collision.gameObject.GetComponent<Target>();
        if (target != null)
        {
            target.TakeDamage(damage);
        }
        ReturnToPool();
        
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        ReturnToPool();
    }

    private void OnTriggerEnter(Collider other)
    {
        ReturnToPool();
    }

    void ReturnToPool()
    {
        if (lifeCoroutine != null)
        {
            StopCoroutine(lifeCoroutine);
            lifeCoroutine = null;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (pool != null)
        {
            pool.ReturnObject(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
