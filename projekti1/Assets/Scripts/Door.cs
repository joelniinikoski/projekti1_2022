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
                //change this to "if spawner in this room is inactive"
                if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = doorOpen;
                    triggered = false;
                    Teleport(GameObject.FindGameObjectWithTag("Player"), teleport);
                }
                
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
