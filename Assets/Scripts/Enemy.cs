using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public bool stunned;
    public int damage;
    public float aggroRange;
    public bool aggroOnBaseLight;
    public bool onCeiling;

    [HideInInspector]public bool angry;
    float hp = 30;
    float stunDuration = 5f;

    float attackCD = 1f;
    float attackCurrentCD;

    SpriteRenderer img;
    Rigidbody2D rb2D;
    Animator animator;
    Player player;
    FlashLight flashLight;

    void Start()
    {
        player = Player.instance;
        flashLight = player.flashLight;
        img = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!angry && Vector2.Distance(transform.position, player.transform.position) <= aggroRange)
            angry = true;
        if (angry && !stunned)
            Aggro();
        if (attackCurrentCD > 0)
            attackCurrentCD -= Time.deltaTime;
        if (player.transform.position.x < transform.position.x)
            img.flipX = true;
        else
            img.flipX = false;
        // if (attackCurrentCD <= 0)
        //     animator.SetBool("Idle", false);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (stunned)
            return;
        if (other.tag == "Light")
        {
            if (other.gameObject.Equals(flashLight.gameObject) && !flashLight.Focusing)// si c'est la flashlight on verifie si elle est focus sinon ca marche direct
            {
                if (!angry && aggroOnBaseLight)
                    angry = true;// si c'est la lampe, qu'elle focus pas et qu'on a un mob qui aggro sur baselight
                return ;
            }
            if (!angry)
                angry = true;
            Color col = img.color;
            img.color = new Color(col.r, col.g, col.b, col.a += 0.0075f);
        }
        if (img.color.a >= 1)
            StartCoroutine("Stunning");
    }

    void Aggro()
    {
        if (onCeiling)
        {
            rb2D.velocity = Vector2.zero;
            rb2D.AddForce(Vector2.down * 10, ForceMode2D.Impulse);
            return;
        }
        if (Vector2.Distance(transform.position, player.transform.position) < 2.4)
            Attack();
        else
            Charge();
    }

    void Charge()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Running", true);
        rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        float move = 4;
        if (player.transform.position.x < transform.position.x)
            move *= -1;
        rb2D.AddForce(Vector2.right * move * 60f * Time.deltaTime, ForceMode2D.Impulse);
    }

    void Attack()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Running", false);
        rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        if (attackCurrentCD > 0)
            return;
        attackCurrentCD = attackCD;
        player.TakeDamage(damage);
        animator.SetBool("Attacking", true);
        //declencher l'animation attacking puis apres l'attaue + la pause de 1 sec de vide apres l'attaque le state machine behaviour le remet a false
    }

    IEnumerator Stunning()
    {
        stunned = true;
        rb2D.velocity = Vector2.zero;
        img.color = Color.red;
        float time = stunDuration;
        while (time > 0)
        {
            time -= Time.deltaTime * 1;
            yield return null;
        }
        stunned = false;
        img.color = Color.white;
        img.color = new Color(img.color.r, img.color.g, img.color.b, 1);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            rb2D.velocity = new Vector2(0, 0);
            onCeiling = false;
        }
    }

    void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
