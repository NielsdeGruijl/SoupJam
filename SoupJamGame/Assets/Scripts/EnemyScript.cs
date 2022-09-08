using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject player;

    HealthManager manager;
    [SerializeField] EnemySpawner spawner;

    Vector2 dir;

    float speed = 1f;

    float value = 10;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        manager = GetComponent<HealthManager>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>();

        IncreaseHP();
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        dir = (player.transform.position - transform.position).normalized;
        rb.velocity = dir * speed;
    }

    public void CommitDie()
    {
        player.GetComponent<PlayerScript>().AddScore(value);
        Destroy(gameObject);
    }

    void IncreaseHP()
    {
        manager.health += spawner.HPIncrease;
    }
}
