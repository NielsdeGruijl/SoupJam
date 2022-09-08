using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float damageTaken;
    [SerializeField] float enemyDamageCD;

    [SerializeField] Text scoreText;

    bool canDmg = true;

    float playerScore;

    Vector3 cameraPos;
    Vector3 playerPos;

    private void Update()
    {
        cameraPos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -10);
        playerPos = new Vector3(transform.position.x, transform.position.y, -10);

        Camera.main.transform.position = Vector3.Lerp(cameraPos, playerPos, Time.deltaTime * 2f);
    }

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

    public void AddScore(float score)
    {
        playerScore += score;

        scoreText.text = "SCORE: " + playerScore;
    }

    public void Die()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
