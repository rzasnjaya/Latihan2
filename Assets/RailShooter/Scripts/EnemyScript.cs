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
        if (anim == null || !anim.enabled  || !agent.enabled)
        {
            return;
        }

        if (agent.remainingDistance > 0.01f)
        {
            movementLocal = Vector3.Lerp(movementLocal, transform.InverseTransformDirection(agent.velocity).normalized, 2f * Time.deltaTime);

            agent.nextPosition = transform.position;
        }
        else
        {
            movementLocal = Vector3.Lerp(movementLocal, Vector3.zero, 2f * Time.deltaTime);
        }

        anim.SetFloat("X Speed", movementLocal.x);
        anim.SetFloat("Z Speed", movementLocal.z);

        
    }

    public void Hit(RaycastHit hit, int damage = 1)
    {
        if (isDead)
            return;

        currentHealth -= damage;
        Debug.Log("Enemy Shot!");

        if (currentHealth <= 0)
        {
            isDead = true;
            agent.enabled = false;
            shootOutPoint.EnemyKilled();
            anim.SetTrigger("Dead");
            anim.SetBool("Is Dead", true);
            Destroy(gameObject, 4f);
        }
        else
        {
            anim.SetTrigger("Shot");
        }
    }
}
