using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public FlashLight flashLight;
    public GameObject enemiesHolder;
    public SpriteRenderer head;

    float speed = 5f;
    [HideInInspector]public Transform checkPoint;

    [HideInInspector]public bool active = true;// if can move hit, use light etc
    Rigidbody2D rb2D;
    SpriteRenderer sprite;
    Animator animator;

    void Awake()
    {
        instance = this;
        rb2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        // Debug.Log(rb2D.velocity);
        // rb2D.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        // Debug.Log(rb2D.velocity);
    }

    void Update()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x / 10, rb2D.velocity.y);
        if (active)
            Move();
    }

    void Move()
    {
        float movement = Input.GetAxis("Horizontal") * speed;
        if (movement < 0)
        {
            sprite.flipX = true;
            animator.SetBool("Running", true);
        }
        else if (movement > 0)
        {
            sprite.flipX = false;
            animator.SetBool("Running", true);
        }
        else
            animator.SetBool("Running", false);

        rb2D.AddForce(Vector2.right * movement * 60f * Time.deltaTime, ForceMode2D.Impulse);
        //rb2D.MovePosition(rb2D.position  + Vector2.right * movement * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        flashLight.battery -= damage;
        if (flashLight.battery <= 0)
            OnDeath();
    }

    public void OnDeath()
    {
        flashLight.battery = flashLight.startingBattery;
        transform.position = checkPoint.position;
        flashLight.Reset();
        foreach(Enemy enemy in enemiesHolder.transform.GetComponentsInChildren<Enemy>())
        {
            enemy.angry = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Cells")
        {
            Destroy(col.gameObject);
            flashLight.CellPickUp();
        }
        else if (col.gameObject.tag == "CheckPoint")
            checkPoint = col.transform;
    }

}
