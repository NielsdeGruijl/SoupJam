using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    Camera cam;

    Vector3 originalPos;
    Vector2 offset;

    private void Start()
    {
        if (instance == null)
            instance = this;
       if(instance != null && instance != this)
            Destroy(this);

        cam = Camera.main;
    }

    public IEnumerator Shake(float duration)
    {
        originalPos = cam.transform.position;
        float elapsedTime = 0.0f;

        while(elapsedTime < duration)
        {
            offset = new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            cam.transform.position += new Vector3(offset.x, offset.y, cam.transform.position.z);

            elapsedTime += Time.deltaTime;

            Debug.Log("SHAKE!");

            yield return null;
        }

        cam.transform.position = originalPos;
    }
}
