using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    
    public Transform player;
    
    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    public float walkPointRange;
    private bool walkPointSet;

    public float health;
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    //public GameObject projectile;
    
    public float bulletDamage = 30f;
    public float bulletSpeed = 600f;
    public float attackRange = 100f;
    public Transform shootingPoint;
    public ObjectPool bulletPool;

    public float sightRange;
    public bool  playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent =  GetComponent<NavMeshAgent>();
        if (bulletPool == null)
        {
            bulletPool = FindObjectOfType<ObjectPool>();
        }
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = CheckAttackRange();
        
        if(!playerInSightRange && !playerInAttackRange) Patroling();
        if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        if(playerInSightRange && playerInAttackRange) AttackPlayer();
    }
    private bool CheckAttackRange()
    {
        if (player == null) return false;
        
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > attackRange) return false;
        
        RaycastHit hit;
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, attackRange))
        {
            return hit.transform == player;
        }
        
        return false;
    }
    
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
        
        if(walkPointSet)
            agent.SetDestination(walkPoint);
        
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        
        if(distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        
        
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        
        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        directionToPlayer.y = 0; 
        if (directionToPlayer != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        if (!alreadyAttacked)
        {
            Shoot();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void Shoot()
    {
        if (player == null || bulletPool == null) return;
        
        
        Vector3 targetPoint = player.position + Vector3.up * 0.5f; 
        
        Vector3 direction = (targetPoint - shootingPoint.position).normalized;
        
        GameObject bullet = bulletPool.GetObject();
        if (bullet == null) return;
        
        bullet.transform.SetPositionAndRotation(shootingPoint.position, Quaternion.LookRotation(direction));
        
        
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.linearVelocity = bullet.transform.forward * bulletSpeed;
        }
        
        
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.pool = bulletPool;
            bulletScript.lifeTime = 5f;
            bulletScript.damage = bulletDamage;
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        
        if (health <= 0) Invoke(nameof(DestroyEnemy), 1.5f);
    }
    
    public void TakeHeadshot()
    {
        health = 0;
        Invoke(nameof(DestroyEnemy), 0.8f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}

