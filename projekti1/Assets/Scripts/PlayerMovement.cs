using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator animator;
    public float health;
    public AudioSource damageSource;
    public float damageTimerReset = 0.5f;
    public Text hpText;

    float damageTimer = 0f;
    Vector2 moveVector;

    // Update is called once per frame
    void Update()
    {
        //i frames
        if (damageTimer >= 0)
        {
            damageTimer -= Time.deltaTime;
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
        rb.MovePosition(rb.position + moveVector.normalized * moveSpeed);
    }

    public void TakeDamage(float dmg)
    {
        if (damageTimer <= 0) {
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
