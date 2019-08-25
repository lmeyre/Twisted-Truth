using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public FlashLight flashLight;
    public GameObject enemiesHolder;
    public SpriteRenderer head;
    public SpriteRenderer handLamp;
    public Animator katana;
    public CameraFollow camera;

    public Transform headIdle;
    public Transform headRun;

    float speed = 5f;
    [HideInInspector]public Transform checkPoint;

    [HideInInspector]public bool active = true;// if can move hit, use light etc
    Rigidbody2D rb2D;
    SpriteRenderer sprite;
    Animator animator;
    Coroutine damageAnim;

    void Awake()
    {
        instance = this;
        rb2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        //katana.transform.position = new Vector2(katana.transform.position.x + Globals.katanaOffset, katana.transform.position.y);
        // Debug.Log(rb2D.velocity);
        // rb2D.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        // Debug.Log(rb2D.velocity);
    }

    void Update()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x / 10, rb2D.velocity.y);
        if (active)
            Move();
        if (Input.GetMouseButtonDown(0))
            Attack();
    }

    void Move()
    {
        float movement = Input.GetAxis("Horizontal") * speed;
        if (movement < 0)
        {
            //sprite.flipX = true;
            transform.localScale = new Vector3(-1, 1, 1);
            flashLight.transform.localScale = new Vector3(-1, 1, 1);
            head.transform.localScale = new Vector3(-1, 1, 1);
            //handLamp.transform.localScale = new Vector3(-1, 1, 1);
            animator.SetBool("Running", true);
            head.transform.position = headRun.position;
        }
        else if (movement > 0)
        {
            //sprite.flipX = false;
            transform.localScale = new Vector3(1, 1, 1);
            head.transform.localScale = new Vector3(1, 1, 1);
            flashLight.transform.localScale = new Vector3(1, 1, 1);
            //handLamp.transform.localScale = new Vector3(1, 1, 1);
            animator.SetBool("Running", true);
            head.transform.position = headRun.position;
        }
        else
        {
            animator.SetBool("Running", false);
            head.transform.position = headIdle.position;
        }
            

        rb2D.AddForce(Vector2.right * movement * 60f * Time.deltaTime, ForceMode2D.Impulse);
        //rb2D.MovePosition(rb2D.position  + Vector2.right * movement * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        flashLight.battery -= damage;
        if (damageAnim != null)
            StopCoroutine(damageAnim);
        StartCoroutine(camera.ShakeScreen(1f, 0.07f));
        damageAnim = StartCoroutine("DamageAnimation");
        if (flashLight.battery <= 0)
            OnDeath();
        flashLight.batteryBar.value = flashLight.battery / flashLight.maxBattery;
    }

    IEnumerator DamageAnimation()
	{
		float i = 1f;
		SpriteRenderer[] sprites = new SpriteRenderer[] {GetComponent<SpriteRenderer>(), head, handLamp, katana.GetComponent<SpriteRenderer>()};
         
		while (i > 0)
		{
            foreach(SpriteRenderer spriteX in sprites)
			    spriteX.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time * 5, 1));
			i -= Time.deltaTime;
			yield return null;
		}
        foreach(SpriteRenderer spriteX in sprites)
		    spriteX.color = Color.white;
	}

    public void OnDeath()
    {
        flashLight.battery = flashLight.maxBattery / 3;
        flashLight.batteryBar.value = flashLight.battery / flashLight.maxBattery;
        transform.position = checkPoint.position;
        foreach(Enemy enemy in enemiesHolder.transform.GetComponentsInChildren<Enemy>())
        {
            enemy.angry = false;
        }
    }

    void Attack()
    {
        katana.SetBool("Attacking", true);
    }
}
