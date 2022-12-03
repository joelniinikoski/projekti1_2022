using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("level", 0);
        LoadScene loadScene = GameObject.FindGameObjectWithTag("LoadScene").GetComponent<LoadScene>();
        loadScene.Load(5);
        loadScene.ChangeScene();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
