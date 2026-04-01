using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 10f;
    public GameObject hitEffect, healthBar;
    public bool isEnemy = true;

    private string tagName;
    private float currentHealth;

    // Start is called before the first frame update
    void OnEnable ()
    {
        if (isEnemy)
            tagName = "Bullet";
        else
            tagName = "EnemyBullet";

        currentHealth = maxHealth;
    }    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagName))
        {
            float damage = float.Parse(other.name);
            TakeDamage(damage);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
}
