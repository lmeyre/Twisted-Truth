using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    public Transform linkedStair;
    public SpriteRenderer currentMask;
    public bool ready = true;
    Player player;

    void Start()
    {
        ready = true;
        player = Player.instance;
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && Vector2.Distance(Player.instance.transform.position, transform.position) < 3 && ready)
        {
            Teleport();
        }
    }

    void Teleport()
    {
        linkedStair.GetComponent<Stairs>().ready = false;
        StartCoroutine(linkedStair.GetComponent<Stairs>().ReadySoon());
        StartCoroutine(FadeIn(currentMask));
        StartCoroutine(FadeOut(linkedStair.GetComponent<Stairs>().currentMask));
        StartCoroutine("MovePlayer");
    }

    IEnumerator FadeIn(SpriteRenderer toFade) 
    {
        Color c = toFade.color;
        for (float f = 0f; f <= 1; f += 0.05f) 
        {
            c.a = f;
            toFade.color = c;
            yield return new WaitForFixedUpdate();
        }
//        Debug.Log(toFade.color); // sans le rajout ca va pas a 1 car ca tourne pas la derniere frame
        c.a = 1;
        toFade.color = c;
    } 

    IEnumerator FadeOut(SpriteRenderer toFade) 
    {
        Color c = toFade.color;
        for (float f = 1f; f > 0; f -= 0.05f) 
        {
            c.a = f;
            toFade.color = c;
            yield return new WaitForFixedUpdate();
        }
        c.a = 0;
        toFade.color = c;
    }

    IEnumerator ReadySoon()
    {
        yield return new WaitForSeconds(2);
        ready = true;
    }

    IEnumerator MovePlayer()
    {
        player.active = false;
        player.GetComponent<Collider2D>().enabled = false;
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.head.enabled = false;
        player.katana.GetComponent<SpriteRenderer>().enabled = false;
        player.flashLight.GetComponent<SpriteRenderer>().enabled = false;
        player.flashLight.GetComponent<SpriteMask>().enabled = false;
        player.handLamp.enabled = false;
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        while (Vector2.Distance(player.transform.position, linkedStair.position) > 1)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, linkedStair.position, 0.2f);
            yield return null;
        }
        player.active = true;
        player.GetComponent<Collider2D>().enabled = true;
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.head.enabled = true;
        player.katana.GetComponent<SpriteRenderer>().enabled = true;
        player.flashLight.gameObject.SetActive(true);
        player.flashLight.GetComponent<SpriteRenderer>().enabled = true;
        player.flashLight.GetComponent<SpriteMask>().enabled = true;
        player.handLamp.enabled = true;
        player.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
