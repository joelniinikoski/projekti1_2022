using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    [SerializeField] float healing = 10f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Player playerScript = collision.gameObject.GetComponent<Player>();
            playerScript.TakeDamage(-healing, 0f);
            Destroy(gameObject);
        }
    }
}
