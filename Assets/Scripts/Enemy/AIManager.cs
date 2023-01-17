using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIManager : MonoBehaviour
{
    public static event Action<Vector3> ManagerUpdate;
    public static event Action<bool> PlayerDied;
    public static event Action PlayerWin;
    [SerializeField] private Transform player;
    private int diedBossCount = 0;
    private int totalBossCount = 0;
    private HcLevelManager hcLevelManager;
    private bool isPlayerAlive = true;

    public float setBossCount
    {
        get => totalBossCount;
        set
        {
            value = Mathf.Clamp(value, 1, 5);
            totalBossCount = (int)value;

            if (diedBossCount >= totalBossCount)
            {
                PlayerWin?.Invoke();
            }
        }
    }

    private void OnEnable()
    {
        GameManager.GameOver += GameEnded;
    }

    private void OnDisable()
    {
        GameManager.GameOver -= GameEnded;
    }

    private void Start()
    {
        hcLevelManager = HcLevelManager.Instance;
        setBossCount = (hcLevelManager.GetGlobalLevelIndex() + 1) / 2;
    }

    void FixedUpdate()
    {
        if (isPlayerAlive)
        {
            ManagerUpdate?.Invoke(player.position);
        }
    }

    private void GameEnded(bool winCheck)
    {
        isPlayerAlive = false;
        PlayerDied?.Invoke(winCheck);
    }

    public void BossDied()
    {
        diedBossCount++;
    }
}
