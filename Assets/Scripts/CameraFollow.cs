using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    Vector3 shake;

    void Start()
    {
        shake = new Vector3();
    }

    void Update()
    {
        transform.position = player.position + new Vector3(0, 5, -10) + shake;
    }

    public IEnumerator ShakeScreen(float duration, float magnitude)
    {
        float x;
        float y;
        while (duration > 0)
        {
            x = Random.Range(-1, 1) * magnitude;
            y = Random.Range(-1, 1) * magnitude;
            shake.x = x;
            shake.y = y;
            yield return null;
            duration -= 1 * Time.deltaTime;
        }
        shake.x = 0;
        shake.y = 0;
    }
}
