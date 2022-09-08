using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUps : MonoBehaviour
{
    float powerupSpawnTimer = 20f;

    float powerupDuration;

    float dmgMultiplier;

    public bool spawnPowerup = true;
    bool activeBoost = false;

    [SerializeField] ShootingMode shootingScript;

    [SerializeField] GameObject damageBoost;

    [SerializeField] Text duration;

    [SerializeField] GameObject powerupUI;

    enum Powerups
    {
        dmgBoost
    }

    Powerups powerups;

    private void Start()
    {
        powerupUI.SetActive(false);
    }

    private void Update()
    {
        if (spawnPowerup)
        {
            StartCoroutine(SpawnPowerups());
        }

        if (activeBoost)
        {
            powerupDuration -= Time.deltaTime;


            duration.text = powerupDuration.ToString();    

            if(powerupDuration <= 0)
            {
                shootingScript.damageMultiplier = 1f;
                shootingScript.Shoot();
                powerupUI.SetActive(false);
            }
        }
    }

    public void ActivateBuff()
    {
        switch (powerups)
        {
            case (Powerups.dmgBoost):
                powerupDuration = 10f;
                dmgMultiplier = 2f;
                dmgBoost();
                break;
        }
    }

    void dmgBoost()
    {
        shootingScript.damageMultiplier = dmgMultiplier;
        shootingScript.Shoot();
        powerupUI.SetActive(true);
        activeBoost = true;
    }

    IEnumerator SpawnPowerups()
    {
        spawnPowerup = false;
        yield return new WaitForSeconds(powerupSpawnTimer);
        Vector2 spawnPos = new Vector2(Random.Range(-20, 20), Random.Range(-9.5f, 9.5f));
        Instantiate(damageBoost, spawnPos, Quaternion.identity);
    }
}
