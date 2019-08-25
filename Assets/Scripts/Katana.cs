using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour
{
    public Player player;

    public Transform idleKatanaPos;
    public Transform attackKatanaPos;
   // public bool damageReady;

    void Start()
    {
     //   damageReady = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
     //   if (!damageReady)
     //       damageReady = false;
        if (col.tag == "Enemy")
        {
            Debug.Log("TRIGGERING");
            col.GetComponent<Enemy>().Invoke("TakeDamage", 0.2f);
         //   damageReady = false;
        }
    }
}
