using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndDoor : MonoBehaviour, IInteractable
{
    LoadScene loadScene;
    [SerializeField] int sceneID = 1;

    private void Start()
    {
        loadScene = GameObject.FindGameObjectWithTag("LoadScene").GetComponent<LoadScene>();
    }

    public void Interact()
    {
        loadScene.Load(sceneID);
        loadScene.ChangeScene();
    }
}
