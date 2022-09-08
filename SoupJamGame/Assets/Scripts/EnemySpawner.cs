using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float timer = 1f;
    bool canSpawn = true;

    float totalEnemiesSpawned;

    public float HPIncrease;

    private void Update()
    {
        if (canSpawn)
        {
            StartCoroutine(SpawnTimer());
        }
    }

    IEnumerator SpawnTimer()
    {
        canSpawn = false;
        GetComponent<ObjectSpawner>().SpawnObject();
        totalEnemiesSpawned++;
        IncreaseHP();
        yield return new WaitForSeconds(timer);

        if (timer > 0.3f)
            timer -= 0.005f;
        
        canSpawn = true;
    }

    void IncreaseHP()
    {
        switch (totalEnemiesSpawned)
        {
            case (10):
                HPIncrease = 1;
                break;
            case (20):
                HPIncrease = 2;
                break;
            case (30):
                HPIncrease = 3;
                break;
            case (40):
                HPIncrease = 4;
                break;
            case (50):
                HPIncrease = 5;
                break;
        }
    }
}
