using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public float speedTowardsPlayer;
    public float health = 100;
    Transform playerT;
    public AudioSource damageSource;
    public Material whiteMat;
    public float flashTime = 0.1f;
    

    float matTimer = 0f;
    Material originalMat;
    bool isDead = false;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Collider2D cl;
    Animator animator;

    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        playerT = GameObject.FindGameObjectWithTag("Player").transform;

        rb = this.GetComponent<Rigidbody2D>();
        cl = this.GetComponent<Collider2D>();
        sr = this.GetComponent<SpriteRenderer>();
        animator = this.GetComponent<Animator>();

        originalMat = sr.material;
    }

    private void Update()
    {
        if (!isDead)
        {
            if (matTimer <= 0)
            {
                sr.material = originalMat;
            }
            else
            {
                matTimer -= Time.deltaTime;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDead)
        {
            direction = playerT.position - transform.position;
            rb.AddForce(direction.normalized * speedTowardsPlayer, ForceMode2D.Impulse);
        }
    }
    public void TakeDamage(float dmg, float kb)
    {
        //white flash material
        sr.material = whiteMat;
        matTimer = flashTime;

        health -= dmg;

        //knockback
        rb.AddForce(direction.normalized * kb * -speedTowardsPlayer, ForceMode2D.Impulse);

        damageSource.pitch = Random.Range(0.8f, 1.2f);
        damageSource.Play();

        //disable spriterenderer and collider so sound can play before death;
        if (health <= 0)
        {
            cl.enabled = false;
            isDead = true;
            animator.SetTrigger("isDead");
            rb.velocity = Vector2.zero;

            Destroy(gameObject, 1f);
        }
    }
}

