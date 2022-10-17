using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour, IDamageable
{
    public float speedTowardsPlayer;
    public float health = 100;
    Transform playerT;
    public AudioSource damageSource;
    public AudioSource deathSource;
    public Material whiteMat;
    public float flashTime = 0.1f;
    public float destroyTime = 1f;
    [SerializeField] float damage = 5;

    Vector3 deathScale;
    float matTimer = 0f;
    Material originalMat;
    [System.NonSerialized]
    public bool isDead = false;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Collider2D cl;
    Animator animator;

    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        if (!deathSource)
        {
            deathSource = damageSource;
        }
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

    private void LateUpdate()
    {
        if (isDead)
        {
            transform.localScale = deathScale;
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
   
        if (health <= 0)
        {
            if (deathSource.isActiveAndEnabled)
            {
                deathSource.pitch = Random.Range(0.8f, 1.2f);
                deathSource.Play();
            }
                

            //disable spriterenderer and collider so sound can play before death;

            cl.enabled = false;
            isDead = true;
            //animator.SetTrigger("isDead");
            StartCoroutine(Death(0.5f));
            rb.velocity = Vector2.zero;

            Destroy(gameObject, destroyTime);
        } else
        {
            if (damageSource.isActiveAndEnabled)
            {
                damageSource.pitch = Random.Range(0.8f, 1.2f);
                damageSource.Play();
            }

            //knockback
            rb.AddForce(direction.normalized * kb * -speedTowardsPlayer, ForceMode2D.Impulse);
        }
    }
    private IEnumerator Death(float animLength)
    {
        float startAnimLength = animLength;
        Vector3 startScale = gameObject.transform.localScale;
        while (true)
        {
            animLength -= Time.deltaTime;
            deathScale = startScale * (Mathf.Max(0, animLength / startAnimLength));
            if (animLength <= 0)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Player playerScript = collision.gameObject.GetComponent<Player>();
            playerScript.TakeDamage(damage);
        }
    }
}

