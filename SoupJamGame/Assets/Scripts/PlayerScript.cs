using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float damageTaken;
    [SerializeField] float enemyDamageCD;

    [SerializeField] Text scoreText;

    [SerializeField] GameObject gun;
    [SerializeField] PowerUps powerups;

    Camera cam;

    bool canDmg = true;

    float playerScore;

    Vector3 cameraPos;
    Vector3 playerPos;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        FlipPlayer();
    }

    private void FixedUpdate()
    {
        CameraFollowPlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<HealthManager>().TakeDamage(damageTaken);
        }
        if (collision.gameObject.CompareTag("Powerup"))
        {
            powerups.ActivateBuff();
            powerups.spawnPowerup = true;
            Destroy(collision.gameObject);
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
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    void CameraFollowPlayer()
    {
        /*QualitySettings.vSyncCount = 1;*/

        cameraPos = new Vector3(cam.transform.position.x, cam.transform.position.y, -10);
        playerPos = new Vector3(transform.position.x, transform.position.y, -10);

        cam.transform.position = Vector3.Lerp(cameraPos, playerPos, Time.deltaTime * 2f);
    }

    void FlipPlayer()
    {
        if(cam.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            gun.GetComponent<SpriteRenderer>().flipY = true;
        }
        if (cam.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            gun.GetComponent<SpriteRenderer>().flipY = false;
        }
    }
}
