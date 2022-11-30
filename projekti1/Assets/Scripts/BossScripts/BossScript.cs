using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    [SerializeField] GameObject destroySpawner;
    [SerializeField] GameObject levelEndDoor;
    [SerializeField] public GameObject healthBarBack;

    [System.NonSerialized]
    public float startHealth;
    [System.NonSerialized]
    public float health;
    [System.NonSerialized]
    public float prevHealth;
    [System.NonSerialized]
    public Image hpBar;
    [System.NonSerialized]
    public IHasHealth boss;

    public virtual void Start()
    {
        boss = gameObject.GetComponent<IHasHealth>();
        startHealth = boss.GetHealth();
        hpBar = healthBarBack.transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        health = boss.GetHealth();
        if (healthBarBack)
        {
            healthBarBack.transform.position = Camera.main.WorldToScreenPoint(transform.position) + new Vector3(0f, 50f, 0f);
            hpBar.fillAmount = health / startHealth;
        }

        if (health <= 0 && prevHealth > 0)
        {
            if (levelEndDoor)
            {
                levelEndDoor.SetActive(true);
            }
            Destroy(destroySpawner);
            Destroy(healthBarBack, 1f);
            EventManager.Instance.BossHasDied();
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemy.GetComponentInChildren<AudioSource>().enabled = false;
                enemy.GetComponent<IDamageable>().TakeDamage(9999999999f, 0f);
            }
        }
        prevHealth = health;
    }
}
