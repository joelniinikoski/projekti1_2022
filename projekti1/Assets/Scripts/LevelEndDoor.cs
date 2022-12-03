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
        if (PlayerPrefs.HasKey("level") && sceneID > PlayerPrefs.GetInt("level") || !PlayerPrefs.HasKey("level"))
        {
            if (sceneID >= 1 && sceneID <= 4)
            {
                PlayerPrefs.SetInt("level", sceneID);
            }
            loadScene.Load(sceneID);
            loadScene.ChangeScene();
        }
    }
}
