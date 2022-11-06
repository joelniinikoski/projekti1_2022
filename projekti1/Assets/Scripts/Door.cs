using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] Sprite doorOpen;
    [SerializeField] Transform teleport;
    [SerializeField] GameObject activateSpawner;

    public bool doorActive = true;

    public void Interact()
    {
        //change this to "if spawner in this room is inactive"
        if (doorActive)
        {
            if (doorOpen)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = doorOpen;
            }
            if (activateSpawner)
            {
                activateSpawner.SetActive(true);
            }
            gameObject.GetComponentInChildren<AudioSource>()?.Play();
            Teleport(GameObject.FindGameObjectWithTag("Player"), teleport);
        }
    }

    void Teleport(GameObject g, Transform t)
    {
        g.transform.position = t.position;
    }
}
