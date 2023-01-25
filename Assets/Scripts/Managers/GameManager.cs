using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;
using System;

public class GameManager : MonoSingleton<GameManager>
{
    public static event Action<bool> GameOver;

    [Header("PlayerPrefs")]
    [SerializeField] private bool clearPlayerPrefs;

    [Header("Money Settings")]
    [SerializeField] private int addMoney = 0;

    private int totalMoney;
    private EnemyInstantiator enemyInstantiator;
    private UIManager uIManager;

    private void OnEnable()
    {
        PlayerManager.PlayerDied += OnPlayerDied;
        AIManager.PlayerWin += OnPlayerWin;
    }

    private void OnDisable()
    {
        PlayerManager.PlayerDied -= OnPlayerDied;
        AIManager.PlayerWin -= OnPlayerWin;
    }

    void Start()
    {
        if (clearPlayerPrefs)
        {
            //PlayerPrefs.DeleteAll();
            //SetTotalMoney(addMoney);
            SkillManager.Instance.DeInit();
        }
        uIManager = UIManager.Instance;
        enemyInstantiator = FindObjectOfType<EnemyInstantiator>();
    }

    public void StartTheGame()
    {
        PlayerManager.Instance.Init();
        SkillManager.Instance.Init();
    }

    public void ReleaseTheEnemies()
    {
        enemyInstantiator.Init();
    }

    public void SetTotalMoney(int collectedAmount)
    {
        totalMoney = PlayerPrefs.GetInt("TotalMoney", 0) + collectedAmount;
        PlayerPrefs.SetInt("TotalMoney", totalMoney);
        UIManager.Instance.SetMoneyUI(totalMoney, true);

        totalMoney = 0;
    }

    private void OnPlayerDied()
    {
        uIManager.RestartButtonUI();
        FinishGame(false);
    }

    private void OnPlayerWin()
    {
        uIManager.NextLvUI();
        FinishGame(true);
    }

    private void FinishGame(bool winCondition)
    {
        SkillManager.Instance.DeInit();
        GameOver?.Invoke(winCondition);
    }

    public static void Haptic(int type)
    {
        if (type == 0)
        {
            MMVibrationManager.Haptic(HapticTypes.LightImpact);
        }
        else if (type == 1)
        {
            MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        }
        else if (type == 2)
        {
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        }
    }
}
