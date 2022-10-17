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
        if (collision.gameObject.layer == 7 && gameObject.layer == 8)
        {
            IDamageable enemyScript = collision.gameObject.GetComponent<IDamageable>();
            enemyScript.TakeDamage(dmg, kb);
        }
        if (gameObject.layer == 8 && collision.gameObject.layer != 6 && collision.gameObject.tag != "Trigger")
        {
            Destroy(Instantiate(bulletDestroyed, transform.position, transform.rotation),1f);
            Destroy(gameObject);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6 && gameObject.layer == 7)
        {
            PlayerMovement playerScript = collision.gameObject.GetComponent<PlayerMovement>();
            playerScript.TakeDamage(dmg);
        }
    }
}
