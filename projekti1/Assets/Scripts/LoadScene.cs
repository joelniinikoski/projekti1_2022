using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    AsyncOperation loadingOperation;
    bool isloading = false;

    public void Load(int i)
    {
        loadingOperation = SceneManager.LoadSceneAsync(i);
        isloading = true;
    }

    private void Update()
    {
        if (isloading && loadingOperation.isDone)
        {
            loadingOperation.allowSceneActivation = true;
        }
    }
}
