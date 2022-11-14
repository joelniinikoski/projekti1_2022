using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void PlayerDeath();
    public static event PlayerDeath OnPlayerDeath;

    public delegate void BossDeath();
    public static event BossDeath OnBossDeath;

    public delegate void LevelUp();
    public static event LevelUp OnPlayerLevelUp;

    public static EventManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayerHasDied()
    {
        OnPlayerDeath?.Invoke();
    }

    public void BossHasDied()
    {
        OnBossDeath?.Invoke();
    }

    public void PlayerHasLeveledUp()
    {
        OnPlayerLevelUp?.Invoke();
    }
}
