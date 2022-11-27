using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    [SerializeField] float damage = 10f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Player playerScript = collision.gameObject.GetComponent<Player>();
            playerScript.TakeDamage(damage, 0f);
        }
    }
}
