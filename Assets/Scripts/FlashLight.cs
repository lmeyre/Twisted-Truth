using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public Player player;
    public Sprite baseLight;
    public Sprite focusLight;

    [HideInInspector]public float battery;
    float cellValue = 15f;
    [HideInInspector]public bool Focusing;

    SpriteRenderer sprite;
    SpriteMask mask;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        mask = GetComponent<SpriteMask>();
    }

    void Start()
    {
        battery = 30;
    }

    void Update()
    {
        Vector3 lookAt = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float AngleRad = Mathf.Atan2(lookAt.y - transform.position.y, lookAt.x - transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        transform.rotation = Quaternion.Euler(0, 0, AngleDeg - 45);
        if (Input.GetMouseButtonDown(0) && player.active && !Focusing)
            FocusLight();
        else if (Input.GetMouseButtonUp(0) && Focusing)
            Focusing = false;
    }

    public void FocusLight()
    {
        Focusing = true;
        StartCoroutine("ConcentrateLight");
    }

    IEnumerator ConcentrateLight()
    {
        sprite.sprite = focusLight;
        mask.sprite = focusLight;
        while (Focusing && battery > 0)
        {
            battery -= 0.03f;
            yield return null;
        }
        if (battery <= 0)
            player.OnDeath();
        sprite.sprite = baseLight;
        mask.sprite = baseLight;
    }

    public void CellPickUp()
    {
        battery += cellValue;
    }
}
