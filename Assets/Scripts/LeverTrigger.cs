using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverTrigger : MonoBehaviour
{
    public GameObject transformedPlayer;
    public GameObject finalLight;
    public GameObject arrow;

    public GameObject endUI;

    public Camera main;
    public Camera anim;

    void Update()
    {
        if (Input.GetKeyDown("space") && Vector2.Distance(Player.instance.transform.position, transform.position) < 2)
        {
            arrow.SetActive(false);
            anim.gameObject.SetActive(true);
            main.gameObject.SetActive(false);
            main.tag = "Untagged";
            anim.tag = "MainCamera";
            anim.GetComponent<Animator>().Play("End");
            StartCoroutine("LightOn");
            GetComponent<Animator>().SetBool("Triggered", true);
            SoundManager.instance.PlaySound(SoundManager.instance.lever);
        }
    }

    public IEnumerator LightOn()
    {
        yield return new WaitForSeconds(3);
        GameObject[] lights = GameObject.FindGameObjectsWithTag("InterruptorLight");


        foreach (GameObject light in lights)
        {
            light.GetComponent<SpriteRenderer>().enabled = true;
            light.GetComponent<SpriteMask>().enabled = true;
            light.SetActive(true);
            SoundManager.instance.PlaySound(SoundManager.instance.lightOn);
            yield return new WaitForSeconds(0.1f);
        }
        
        SoundManager.instance.PlaySound(SoundManager.instance.lightOn);
        finalLight.SetActive(true);
        Player.instance.gameObject.SetActive(false);
        transformedPlayer.SetActive(true);
        transformedPlayer.GetComponent<Animator>().SetBool("Stun", true);
        yield return new WaitForSeconds(4);
        //gradually reduce the light
        SpriteRenderer lightRender = finalLight.GetComponent<SpriteRenderer>();
        Color c = lightRender.color;
        for (float f = 1f; f > 0.7; f -= 0.001f) 
        {
            c.a = f;
            lightRender.color = c;
            yield return null;
        }
        yield return new WaitForSeconds(3);
        //player die
        transformedPlayer.GetComponent<Animator>().SetBool("Dead", true);

        yield return new WaitForSeconds(3);
        endUI.SetActive(true);
    }
}
