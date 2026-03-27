using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    [SerializeField] float damageRadius;
    [SerializeField] float delayUntilDestroy;

    void Start()
    {
        Destroy(gameObject, delayUntilDestroy);
        DamageNearbyObjects();
    }

    void DamageNearbyObjects()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, damageRadius);

        foreach (var col in cols)
        {
            IHitable[] hitables = col.GetComponents<IHitable>();

            RaycastHit hit;

            if (Physics.Raycast(transform.position, col.transform.position - transform.position, out hit))

            if (hitables != null && hitables.Length > 0)
            {
                foreach (var hitable in hitables)
                {
                    hitable.Hit(hit, 100);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
