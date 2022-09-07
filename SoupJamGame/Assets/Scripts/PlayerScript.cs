using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float damageTaken;
    [SerializeField] float enemyDamageCD;

    bool canDmg = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<HealthManager>().TakeDamage(damageTaken);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && canDmg)
        {
            StartCoroutine(damageCD());
        }
    }

    IEnumerator damageCD()
    {
        canDmg = false;
        GetComponent<HealthManager>().TakeDamage(damageTaken);
        yield return new WaitForSeconds(enemyDamageCD);
        canDmg = true;
    }

    public void Die()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
