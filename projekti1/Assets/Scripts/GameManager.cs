using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform l1;
    [SerializeField] Transform l2;
    [SerializeField] Transform l3;
    [SerializeField] Transform l4;

    [SerializeField] GameObject w1;
    [SerializeField] GameObject w2;
    [SerializeField] GameObject w3;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (PlayerPrefs.HasKey("level"))
        {
            switch (PlayerPrefs.GetInt("level"))
            {
                case 1:
                    player.transform.position = l1.position;
                    w1.SetActive(false);
                    break;
                case 2:
                    player.transform.position = l2.position;
                    w2.SetActive(false);
                    break;
                case 3:
                    player.transform.position = l3.position;
                    w3.SetActive(false);
                    break;
                case 4:
                    player.transform.position = l4.position;
                    break;
            }
        }
    }
}