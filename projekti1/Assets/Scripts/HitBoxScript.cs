using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxScript : MonoBehaviour
{
    public float dmg = 5;
    public float kb = 50;
    public GameObject bulletDestroyed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && gameObject.tag != "Enemy")
        {
            Enemy1 enemyScript = collision.gameObject.GetComponent<Enemy1>();
            enemyScript.TakeDamage(dmg, kb);
        }
        if (gameObject.tag == "Bullet" && collision.gameObject.tag != "Player")
        {
            Destroy(Instantiate(bulletDestroyed, transform.position, transform.rotation),1f);
            Destroy(gameObject);
        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameObject.tag == "Enemy")
        {
            PlayerMovement playerScript = collision.gameObject.GetComponent<PlayerMovement>();
            playerScript.TakeDamage(dmg);
        }
    }
}
