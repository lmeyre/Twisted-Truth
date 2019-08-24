﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public Player player;
    public Sprite baseLight;
    public Sprite focusedLight;

    [HideInInspector]public float battery;
    float cellValue = 15f;
    [HideInInspector]public float startingBattery = 30f;
    [HideInInspector]public bool Focusing;

    SpriteRenderer sprite;
    [HideInInspector]public SpriteMask mask;
    Animator animator;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        mask = GetComponent<SpriteMask>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        battery = startingBattery;;
    }

    void Update()
    {
        Vector3 lookAt = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float AngleRad = Mathf.Atan2(lookAt.y - transform.position.y, lookAt.x - transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
        if (Input.GetMouseButtonDown(1) && player.active && !Focusing)
            FocusLight();
        else if (Input.GetMouseButtonUp(1) && Focusing)
            Focusing = false;
        if (GetComponent<PolygonCollider2D>().isTrigger == false)
            GetComponent<PolygonCollider2D>().isTrigger = true;
    }

    public void FocusLight()
    {
        Focusing = true;
        animator.SetBool("Focusing", true);
    }

    public IEnumerator ConcentrateLight()
    {
        while (Focusing && battery > 0)
        {
            battery -= 0.03f;
            yield return null;
        }
        animator.SetBool("Focusing", false);
        if (battery <= 0)
            player.OnDeath();
        sprite.sprite = baseLight;
        mask.sprite = baseLight;
        Destroy(animator.GetComponent<PolygonCollider2D>());
        animator.gameObject.AddComponent(typeof(PolygonCollider2D));
        mask.alphaCutoff = 0.0045f;
    }

    public void CellPickUp()
    {
        battery += cellValue;
    }
}
