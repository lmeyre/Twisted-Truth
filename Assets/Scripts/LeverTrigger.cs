using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverTrigger : MonoBehaviour
{
    public GameObject transformedPlayer;
    public GameObject finalLight;

    public GameObject endUI;

    void Update()
    {
        if (Input.GetKeyDown("space") && Vector2.Distance(Player.instance.transform.position, transform.position) < 2)
        {
            Camera.main.GetComponent<Animator>().Play("End");
            StartCoroutine("LightOn");
        }
    }

    public IEnumerator LightOn()
    {
        yield return new WaitForSeconds(2);
        GameObject[] lights = null;

        foreach (GameObject light in lights)
        {
            light.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
        finalLight.SetActive(true);
        Player.instance.gameObject.SetActive(false);
        transformedPlayer.SetActive(true);
        transformedPlayer.GetComponent<Animator>().SetBool("Stun", true);

        //gradually reduce the light
        SpriteRenderer lightRender = finalLight.GetComponent<SpriteRenderer>();
        Color c = lightRender.color;
        for (float f = 1f; f > 0; f -= 0.05f) 
        {
            c.a = f;
            lightRender.color = c;
            yield return null;
        }
        yield return new WaitForSeconds(2);
        //player die
        transformedPlayer.GetComponent<Animator>().SetBool("Dead", true);

        yield return new WaitForSeconds(5);
        endUI.SetActive(true);
    }
}
