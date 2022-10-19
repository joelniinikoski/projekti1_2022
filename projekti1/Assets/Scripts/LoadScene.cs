using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    AsyncOperation loadingOperation;
    bool change = false;

    public void Load(int i)
    {
        StartCoroutine(Routine(i));
    }

    IEnumerator Routine(int i)
    {
        yield return new WaitForSeconds(0.001f);
        loadingOperation = SceneManager.LoadSceneAsync(i);
        loadingOperation.allowSceneActivation = false;
        yield return new WaitUntil(() => change);
        loadingOperation.allowSceneActivation = true;
    }

    public void ChangeScene()
    {
        change = true;
    }
}
