using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage;

    float deathTimer = 3f;

    private void Update()
    {
        deathTimer -= Time.deltaTime;

        if(deathTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<HealthManager>().TakeDamage(damage);
            print("Hit enemy!");
        }

        Destroy(gameObject);
    }
}
