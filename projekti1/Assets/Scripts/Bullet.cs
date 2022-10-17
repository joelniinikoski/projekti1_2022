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

    [SerializeField] GameObject bulletDestroyed;
    
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
        if (collision.gameObject.layer == 7)
        {
            IDamageable enemyScript = collision.gameObject.GetComponent<IDamageable>();
            enemyScript.TakeDamage(damage, kb);
        }
        if (collision.gameObject.layer != 6 && collision.gameObject.layer != 9)
        {
            Destroy(Instantiate(bulletDestroyed, transform.position, transform.rotation), 1f);
            Destroy(gameObject);
        }
    }
}
