using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour
{
    public Player player;

    public Transform idleKatanaPos;
    public Transform attackKatanaPos;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
            col.GetComponent<Enemy>().Invoke("TakeDamage", 0.2f);
    }
}
