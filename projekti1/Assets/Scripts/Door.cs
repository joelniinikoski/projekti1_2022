using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Sprite doorOpen;
    [SerializeField] Transform teleport;

    bool triggered = false;

    private void Update()
    {
        if (triggered)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = doorOpen;
                triggered = false;
                Teleport(GameObject.FindGameObjectWithTag("Player"), teleport);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            triggered = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            triggered = false;
        }
    }

    void Teleport(GameObject g, Transform t)
    {
        g.transform.position = t.position;
    }
}
