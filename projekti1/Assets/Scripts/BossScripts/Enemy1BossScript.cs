using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy1BossScript : MonoBehaviour
{
    [SerializeField] GameObject destroySpawner;
    [SerializeField] GameObject levelEndDoor;
    [SerializeField] GameObject healthBarBack;

    float startHealth;
    float health;
    float prevHealth;
    Image hpBar;
    Enemy1 enemy1;

    private void Start()
    {
        enemy1 = gameObject.GetComponent<Enemy1>();
        startHealth = enemy1.health;
        hpBar = healthBarBack.transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBarBack)
        {
            healthBarBack.transform.position = Camera.main.WorldToScreenPoint(transform.position) + new Vector3(0f, 50f, 0f);
            hpBar.fillAmount = health / startHealth;
        }
        health = enemy1.health;

        if (health <= 0 && prevHealth > 0)
        {
            levelEndDoor.SetActive(true);
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
