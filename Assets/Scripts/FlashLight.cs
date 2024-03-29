﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLight : MonoBehaviour
{
    public Player player;
    public Sprite baseLight;
    public Sprite focusedLight;
    public Slider batteryBar;
    public SpriteRenderer hand;

    [HideInInspector]public float battery;
    float cellValue = 10f;
    [HideInInspector]public float maxBattery = 50f;
    [HideInInspector]public bool Focusing;
    [HideInInspector]public bool crRunning;
    bool readyToStop;

    SpriteRenderer sprite;
    [HideInInspector]public SpriteMask mask;
    Animator animator;
    public PolygonCollider2D baseCol;
    public PolygonCollider2D focusCol;


    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        mask = GetComponent<SpriteMask>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        battery = maxBattery;
        batteryBar.value = battery / maxBattery;
    }

    void Update()
    {
        Vector3 lookAt = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float AngleRad = Mathf.Atan2(lookAt.y - transform.position.y, lookAt.x - transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
       //Debug.Log(Focusing + " player activ = " + player.active);
        if (Input.GetMouseButtonDown(1) && player.active && !Focusing)
            FocusLight();
        else if (Input.GetMouseButtonUp(1) && Focusing == true)
        {
            readyToStop = true;
        }
        if (readyToStop && crRunning)
        {
            Focusing = false;
            readyToStop = false;
        }
        if (AngleDeg <= 85 && AngleDeg >= -95)
        {
            player.head.flipX = false;
           // player.handLamp.flipX = false;
        }
        else
        {
            player.head.flipX = true;
          //  player.handLamp.flipX = true;
        }
        // if (GetComponent<PolygonCollider2D>().isTrigger == false)
        //     GetComponent<PolygonCollider2D>().isTrigger = true;
    }

    public void FocusLight()
    {
        // if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Focusing"))
        // {
        //     Debug.Log("DERP");
        //     return ;
        // }
        // Debug.Log("Starting");
        // a la limite c'est pas grave si ca se remet ces deux la a true car ils le sont deja si on est pendant l'anim
        Focusing = true;
        animator.SetBool("Focusing", true);
    }

    public IEnumerator ConcentrateLight()
    {
        crRunning = true;
        while (Focusing && battery > 0)
        {
            battery -= 0.02f;
            batteryBar.value = battery / maxBattery;
            yield return null;
        }
        animator.SetBool("Focusing", false);
        if (battery <= 0)
            player.OnDeath();
        batteryBar.value = battery / maxBattery;
        Focusing = false;
        Reset();
    }

    public void Reset()
    {
        sprite.sprite = baseLight;
        mask.sprite = baseLight;
        baseCol.enabled = true;
        focusCol.enabled = false;
        // Destroy(GetComponent<PolygonCollider2D>());
        // gameObject.AddComponent(typeof(PolygonCollider2D));
        // GetComponent<PolygonCollider2D>().isTrigger = true;
        mask.alphaCutoff = 0.0045f;
        crRunning = false;
    }

    public void CellPickUp()
    {
        battery += cellValue;
        if (battery > maxBattery)
            battery = maxBattery;
        batteryBar.value = battery / maxBattery;
    }
}
