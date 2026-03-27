using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour, IHitable
{
    [SerializeField] int maxHealth;
    [SerializeField] Transform targetPos;

    private int currentHealth;
    private Transform player;
    private bool isDead;
    private NavMeshAgent agent;
    private ShootOutPoint shootOutPoint;
    private Animator anim;
    private Vector3 movementLocal;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = Camera.main.transform;
        anim = GetComponentInChildren<Animator>();

        agent.updatePosition = false;
        agent.updateRotation = false;
    }

    public void Init(ShootOutPoint point)
    {
        currentHealth = maxHealth;
        shootOutPoint = point;

        if (agent != null)
        {
            agent.SetDestination(targetPos.position);
        }
    }

    void Update()
    {
        if (player != null && !isDead)
        {
            Vector3 direction = player.position - transform.position;
            direction.y = 0f;

            transform.rotation = Quaternion.LookRotation(direction);
        }

        RunBlend();
    }

    void RunBlend()
    {
        if (anim == null || !anim.enabled)
        {
            return;
        }

        if ((agent.nextPosition - transform.position).sqrMagnitude > 0.01f)
        {
            movementLocal = transform.InverseTransformDirection(agent.nextPosition - transform.position);
        }

        anim.SetFloat("X Speed", movementLocal.x);
        anim.SetFloat("Z Speed", movementLocal.z);
    }

    public void Hit(RaycastHit hit)
    {
        if (isDead)
            return;

        currentHealth--;
        Debug.Log("Enemy Shot!");

        if (currentHealth <= 0)
        {
            isDead = true;
            agent.enabled = false;
            shootOutPoint.EnemyKilled();
            Destroy(gameObject);
        }
    }
}
