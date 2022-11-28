using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float timer = 10f;

    [System.NonSerialized]
    public float damage;
    [System.NonSerialized]
    public float kb;
    [System.NonSerialized]
    public float bulletSpeed;

    float damageLayer;
    float ignoreLayer;
    [SerializeField] GameObject bulletDestroyed;

    private void OnEnable()
    {
        EventManager.OnPlayerDeath += DestroyBullet;
    }
    private void OnDisable()
    {
        EventManager.OnPlayerDeath -= DestroyBullet;
    }

    public void SetDamageLayer(int i)
    {
        damageLayer = i;
        if (i == 7)
        {
            ignoreLayer = 6;
        }
        else ignoreLayer = 7;
    }

    private void Update()
    {
        if (timer <= 0f) {
            Destroy(gameObject);
        }
        timer -= Time.deltaTime;
        transform.Translate(transform.right * bulletSpeed * Time.deltaTime, Space.World);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == damageLayer)
        {
            IDamageable enemyScript = collision.gameObject.GetComponent<IDamageable>();
            enemyScript.TakeDamage(damage, kb);
        }
        if (collision.gameObject.layer != 9 && collision.gameObject.layer != 8 && collision.gameObject.layer != ignoreLayer)
        {
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(Instantiate(bulletDestroyed, transform.position, transform.rotation), 1f);
        Destroy(gameObject);
    }
}
