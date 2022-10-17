using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1BossScript : MonoBehaviour
{
    [SerializeField] GameObject destroySpawner;
    [SerializeField] GameObject levelEndDoor;

    float health;

    // Update is called once per frame
    void Update()
    {
        health = gameObject.GetComponent<Enemy1>().health;
        if (health <= 0)
        {
            levelEndDoor.SetActive(true);
            Destroy(destroySpawner);
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemy.GetComponentInChildren<AudioSource>().enabled = false;
                enemy.GetComponent<IDamageable>().TakeDamage(9999999999f, 0f);
            }
        }
    }
}
