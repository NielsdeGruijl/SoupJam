using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float timer = 1f;
    bool canSpawn = true;

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
        yield return new WaitForSeconds(timer);

        if (timer >= 0.3f)
            timer -= 0.01f;

        canSpawn = true;
    }
}
