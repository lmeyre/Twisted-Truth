using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cells : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Player.instance.flashLight.CellPickUp();
            SoundManager.instance.PlaySound(SoundManager.instance.lightOn);
            Destroy(gameObject);
        }
    }
}
