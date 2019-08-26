using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    float speed = 2f;
    float length = 0.5f;  //length is the difference between min y to max y.
    Vector3 origin;
    float offset;

    void Start()
    {
        origin = transform.position;
    }
    
    void Update()
    {
        if (GetComponent<DestroyArrow>() != null)
        {
            offset = GetComponent<DestroyArrow>().mob.transform.position.x;
            transform.position = new Vector3(0, origin.y) + new Vector3(offset, Mathf.PingPong(speed * Time.time, length));
        }
        else
            transform.position = origin +  new Vector3(0, Mathf.PingPong(speed * Time.time, length));
    }
}
