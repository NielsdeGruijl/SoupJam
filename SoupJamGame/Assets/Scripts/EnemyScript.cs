using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject player;

    Vector2 dir;

    float speed = 1f;

    float HP;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
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
        Destroy(gameObject);
    }
}
