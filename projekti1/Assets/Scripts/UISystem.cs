using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    [SerializeField] TMP_Text textPrefab;
    [SerializeField] Button buttonPrefab;

    private void OnEnable()
    {
        EventManager.OnPlayerDeath += YouDiedText;
        EventManager.OnBossDeath += LevelUpScreen;
    }
    private void OnDisable()
    {
        EventManager.OnPlayerDeath -= YouDiedText;
        EventManager.OnBossDeath -= LevelUpScreen;
    }

    private GameObject canvas;
    private void Awake()
    {

        canvas = GameObject.Find("Canvas");
    }

    void YouDiedText()
    {
        TMP_Text youDiedText = Instantiate(textPrefab, Camera.main.WorldToScreenPoint(Camera.main.transform.position) + new Vector3(0, -50f, 0f), Quaternion.identity);
        youDiedText.transform.SetParent(canvas.transform);
        youDiedText.fontSize = 50;
        youDiedText.text = "You Died";
    }

    void LevelUpScreen()
    {
        Time.timeScale = 0;
        List<GameObject> instantiatedButtons = new List<GameObject>();

        List<(string, string, float)> options = new List<(string, string, float)>()
        {
            //("Seen Name", "Player Pref Key", "Increase magnitude")
            ("Bullet Speed", "BulletSpeed", 1.2f),
            ("Bullet Power", "BulletPower", 1.2f),
            ("Player Speed", "PlayerSpeed", 1.2f)
        };
        List<(string, string, float)> used = new List<(string, string, float)>();

        for (int i = 0; i < 3; i++)
        {
            Button button = Instantiate(buttonPrefab, Camera.main.WorldToScreenPoint(Camera.main.transform.position) + new Vector3(0f, -100+100*i, 0f), Quaternion.identity);
            instantiatedButtons.Add(button.gameObject);
            button.transform.SetParent(canvas.transform);
            int randomIdx = (int)Random.Range(0f, (float)options.Count);
            (string, string, float) currentItem = options[randomIdx];
            button.GetComponentInChildren<TMP_Text>().text = currentItem.Item1;
            used.Add(currentItem);
            options.RemoveAt(randomIdx);
            button.onClick.AddListener(LevelButtonAction);
            void LevelButtonAction()
            {
                //PlayerPrefs.SetFloat(currentItem.Item2, PlayerPrefs.GetFloat(currentItem.Item2) * currentItem.Item3);
                PlayerPrefs.SetFloat(currentItem.Item2, 0.69f);
                Debug.Log(PlayerPrefs.GetFloat(currentItem.Item2));
                Time.timeScale = 1;
            }
        }
        StartCoroutine(WaitUntilChosenThenDestroyButtons(instantiatedButtons));
    }

    IEnumerator WaitUntilChosenThenDestroyButtons(List<GameObject> buttons)
    {
        yield return new WaitUntil(() => Time.timeScale == 1);
        foreach(GameObject b in buttons)
        {
            Destroy(b);
        }
    }
}
