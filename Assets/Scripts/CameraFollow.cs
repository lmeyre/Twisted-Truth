using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    Vector3 shake;
    Vector3 mouseOffset;
    float offsetStrenght = 7;


    void Start()
    {
        shake = new Vector3();
    }

    void Update()
    {
        mouseOffset = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mouseOffset.x =  Mathf.Clamp(mouseOffset.x, 0f, 1f);// to avoid going out of screen with mouse and still looking more far
        mouseOffset.x = mouseOffset.x - 0.5f;
        mouseOffset.x *= offsetStrenght;

        transform.position = player.position + new Vector3(0, 3, -10) + shake + new Vector3(mouseOffset.x, 0, 0);
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
