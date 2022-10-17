using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndDoor : MonoBehaviour, IInteractable
{
    [SerializeField] LoadScene loadScene;
    [SerializeField] int sceneID = 1;

    public void Interact()
    {
        loadScene.Load(sceneID);
    }
}
