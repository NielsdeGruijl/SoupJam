using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    [SerializeField] float explosionRadius = 1f;

    [SerializeField] GameObject explosionVisualizer;

    [HideInInspector] public float damage;

    float deathTimer = 3f;

    private void Update()
    {
        deathTimer -= Time.deltaTime;

        if (deathTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();

        Destroy(gameObject);
    }

    private void Explode()
    {
        print("Hit an object");

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        print("Amount of enemies hit: " + enemiesHit.Length);

        foreach (Collider2D enemy in enemiesHit)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                enemy.gameObject.GetComponent<HealthManager>().TakeDamage(damage);
                print("Kaboom!");
            }
        }

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
