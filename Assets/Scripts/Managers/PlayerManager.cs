using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    public static event Action PlayerDied;

    [Header("Scripts")]
    [SerializeField] private RunnerScript runnerScript;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private GameObject character;
    [SerializeField] private CapsuleCollider characterCollider;
    [SerializeField] private UIManager uiManager;

    [Header("Player Stats")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float healthRegen;
    [SerializeField] private float levelupReqXP;
    [SerializeField] private float gemXpValue;
    private int playerLevel;
    private float currentHealth;
    private float currentXP;

    private bool canRun = false;
    Sequence sequence;

    public float setHealth
    {
        get => currentHealth;
        set
        {
            value = Mathf.Clamp(value, 0, float.MaxValue);
            currentHealth -= value;

            if (currentHealth <= 0) Die();
        }
    }

    public float setXp
    {
        get => currentXP;
        set
        {
            value = Mathf.Clamp(value, 0, levelupReqXP);
            currentXP += value;

            GainXP();
        }
    }

    public void Init()
    {
        playerLevel = PlayerPrefs.GetInt("PlayerLevel", 1);
        currentHealth = maxHealth;
        currentXP = 0;
        uiManager = UIManager.Instance;
    }

    private void FixedUpdate()
    {
        currentHealth += healthRegen / 10;
        UpdateXpAndHealth();
    }

    public void StartMovement()
    {
        runnerScript.StartToRun(true);
        _healthBar.SetActive(true);
        canRun = true;
    }

    public void StopMovement()
    {
        runnerScript.StartToRun(false);
        _healthBar.SetActive(false);
        canRun = false;
    }

    public void GainXP()
    {
        if (currentXP >= levelupReqXP)
        {
            PlayerPrefs.SetInt("PlayerLevel", PlayerPrefs.GetInt("PlayerLevel", 1) + 1);
            playerLevel = PlayerPrefs.GetInt("PlayerLevel", 1);
            currentXP = 0;
            uiManager.OpenUpgradeCardPanel();
            //stop everything here
        }
    }

    public void SetHealthAndStats(float maxHealth, float healthRegen, float increaseAmount)
    {
        this.maxHealth += maxHealth;
        this.healthRegen = healthRegen;
        currentHealth += increaseAmount;
        UpdateXpAndHealth();
    }

    public void UpdateXpAndHealth()
    {
        if (uiManager != null)
        {
            //Debug.Log("Current health: " + currentHealth + "max health: " + maxHealth);
            playerLevel = PlayerPrefs.GetInt("PlayerLevel", 1);
            uiManager.SetPlayerHealth(currentHealth / maxHealth);
            uiManager.SetPlayerXp(currentXP / levelupReqXP, playerLevel);
        }
    }

    private void Die()
    {
        PlayerDied?.Invoke();
        Debug.Log("died");
    }

    public void SetColliderRadius(float multiplier)
    {
        characterCollider.radius = multiplier;
    }

    public int GetLevel()
    {
        return PlayerPrefs.GetInt("PlayerLevel", 0);
    }

    public Transform GetCharacterTransform()
    {
        return character.transform;
    }

    public CapsuleCollider GetCharacterCollider()
    {
        return characterCollider;
    }
}
