using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float health;
    [SerializeField] AudioSource damageSource;
    [SerializeField] float damageTimerReset = 0.5f;
    [SerializeField] Text hpText;
    [SerializeField] Material whiteMat;
    [SerializeField] float flashTime = 0.05f;


    TrailRenderer tr;
    SpriteRenderer sr;
    Material defaultMat;
    Animator animator;
    Rigidbody2D rb;

    //dashing
    bool canDash = true;
    bool isDashing;
    [SerializeField] float dashingPower = 3f;
    [SerializeField] float dashingTime = 0.2f;
    [SerializeField] float dashingCooldown = 1f;

    float matTimer = 0f;
    float damageTimer = 0f;
    Vector2 moveVector;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        tr = gameObject.GetComponent<TrailRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        defaultMat = sr.material;
    }

    // Update is called once per frame
    void Update()
    {
        //i frames
        if (damageTimer > 0)
        {
            damageTimer -= Time.deltaTime;
        }
        //white flash upon damage
        if (matTimer > 0)
        {
            matTimer -= Time.deltaTime;
        }
        else
        {
            sr.material = defaultMat;
        }

        if (isDashing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        //hp text
        hpText.text = "HP: " + health.ToString();

        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", moveVector.x);
        animator.SetFloat("Vertical", moveVector.y);
        animator.SetFloat("Speed", moveVector.magnitude);
        if (moveVector != Vector2.zero)
        {
            animator.SetFloat("IdleHorizontal", moveVector.x);
            animator.SetFloat("IdleVertical", moveVector.y);
        }
    }

    void FixedUpdate()
    {
        if (isDashing) return;
        rb.MovePosition(rb.position + moveVector.normalized * moveSpeed);
    }

    IEnumerator Dash()
    {
        Debug.Log("Hello world");
        canDash = false;
        isDashing = true;
        rb.velocity = moveVector.normalized * dashingPower;
        tr.emitting = true;
        damageTimer = dashingTime;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public void TakeDamage(float dmg)
    {
        if (damageTimer <= 0) {

            sr.material = whiteMat;
            matTimer = flashTime;
            damageTimer = damageTimerReset;

            health -= dmg;

            damageSource.pitch = Random.Range(0.8f, 1.2f);
            damageSource.Play();

            //disable spriterenderer and collider so sound can play before death;
            if (health <= 0)
            {
                
            }
        }
    }
}
