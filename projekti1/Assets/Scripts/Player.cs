using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] float moveSpeed;
    [SerializeField] float health;
    [SerializeField] AudioSource damageSource;
    [SerializeField] float damageTimerReset = 0.5f;
    [SerializeField] Material whiteMat;
    [SerializeField] float flashTime = 0.05f;
    [SerializeField] Image hpBar;

    Prefabs prefabs;
    TrailRenderer tr;
    SpriteRenderer sr;
    Material defaultMat;
    Animator animator;
    Rigidbody2D rb;

    //dashing
    bool canDash = true;
    [System.NonSerialized]
    public bool isDashing;
    [SerializeField] float dashingPower = 3f;
    [SerializeField] float dashingTime = 0.2f;
    [SerializeField] float dashingCooldown = 1f;

    float matTimer = 0f;
    float damageTimer = 0f;
    Vector2 moveVector;
    float startHealth;
    bool isDead = false;
    HashSet<GameObject> interactables = new HashSet<GameObject>();

    private void OnEnable()
    {
        EventManager.OnPlayerLevelUp += UpdateStats;
    }
    private void OnDisable()
    {
        EventManager.OnPlayerLevelUp -= UpdateStats;
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("PlayerSpeed")) PlayerPrefs.SetFloat("PlayerSpeed", moveSpeed);
        else moveSpeed = PlayerPrefs.GetFloat("PlayerSpeed");
        if (!PlayerPrefs.HasKey("DashingPower")) PlayerPrefs.SetFloat("DashingPower", dashingPower);
        else dashingPower = PlayerPrefs.GetFloat("DashingPower");
        if (!PlayerPrefs.HasKey("DashingCooldown")) PlayerPrefs.SetFloat("DashingCooldown", dashingCooldown);
        else dashingCooldown = PlayerPrefs.GetFloat("DashingCooldown");

        animator = gameObject.GetComponent<Animator>();
        tr = gameObject.GetComponent<TrailRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        defaultMat = sr.material;
        startHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (hpBar)
        {
            hpBar.fillAmount = health / startHealth;
        }
        
        if (isDead) { return; }
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
            return;
        }
        if (interactables.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                foreach (GameObject i in interactables)
                {
                    i.GetComponent<IInteractable>().Interact();
                }
            }   
        }
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
        canDash = false;
        isDashing = true;
        rb.velocity = moveVector.normalized * dashingPower;
        tr.emitting = true;
        damageTimer = dashingTime;
        // ignore collision between enemy layer (7) and player layer (6)
        Physics2D.IgnoreLayerCollision(6, 7, true);
        // ignore collision between bullet (8) and player (6)
        Physics2D.IgnoreLayerCollision(6, 8, true);
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        isDashing = false;
        Physics2D.IgnoreLayerCollision(6, 7, false);
        Physics2D.IgnoreLayerCollision(6, 8, false);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public void TakeDamage(float dmg, float kb)
    {
        if (damageTimer <= 0) {

            health -= dmg;

            if (dmg > 0) {
                sr.material = whiteMat;
                matTimer = flashTime;
                damageTimer = damageTimerReset;

                damageSource.pitch = Random.Range(0.8f, 1.2f);
                damageSource.Play();
            }

            //disable spriterenderer and collider so sound can play before death;
            if (health <= 0)
            {
                sr.enabled = false;
                isDead = true;
                moveVector = Vector2.zero;
                gameObject.GetComponent<Collider2D>().enabled = false;
                GameObject.FindGameObjectWithTag("LoadScene").GetComponent<LoadScene>().Load(0);
                StartCoroutine(Die());
            }
        }
    }

    IEnumerator Die()
    {
        EventManager.Instance.PlayerHasDied();
        PlayerPrefs.DeleteAll();
        yield return new WaitForSeconds(3f);
        GameObject.FindGameObjectWithTag("LoadScene").GetComponent<LoadScene>().ChangeScene();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            interactables.Add(collision.gameObject);
        }
    }

    void UpdateStats()
    {
        moveSpeed = PlayerPrefs.GetFloat("PlayerSpeed");
        dashingPower = PlayerPrefs.GetFloat("DashingPower");
        dashingCooldown = PlayerPrefs.GetFloat("DashingCooldown");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (interactables.Contains(collision.gameObject)) {
            interactables.Remove(collision.gameObject);
        }
    }
}
