using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOriginInstance : MonoBehaviour
{
    public static BossOriginInstance Instance;

    void Awake()
    {
        Instance = this;
    }

    [System.NonSerialized] public int bossAmount = 1;
    [SerializeField] GameObject levelEndDoor;

    private void Update()
    {
        if (bossAmount <= 0)
        {
            levelEndDoor.SetActive(true);
        }
    }
}
