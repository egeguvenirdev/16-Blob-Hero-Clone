using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    [Header("Scripts")]
    [SerializeField] private RunnerScript _runnerScript;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private UIManager uiManager;

    [Header("Player Stats")]
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _healthRegen;
    [SerializeField] private float _levelupReqXP;
    private float _currentHealth;
    private float _currentXP;

    private bool canRun = false;
    Sequence sequence;

    public float setHealth
    {
        get => _currentHealth;
        set
        {
            value = Mathf.Clamp(value, 0, float.MaxValue);
            _currentHealth -= value;

            if (_currentHealth <= 0) Die();
        }
    }

    public void Init()
    {
        _currentHealth = _maxHealth;
        _currentXP = _levelupReqXP;
        uiManager = UIManager.Instance;
    }

    private void FixedUpdate()
    {
        _currentHealth += _healthRegen / 10;
        UpdateHealth();
    }

    public void StartMovement()
    {
        _runnerScript.StartToRun(true);
        _healthBar.SetActive(true);
        canRun = true;
    }

    public void StopMovement()
    {
        _runnerScript.StartToRun(false);
        _healthBar.SetActive(false);
        canRun = false;
    }

    public void GainXP()
    {
        if (_currentXP > _levelupReqXP)
        {
            uiManager.OpenUpgradeCardPanel();
            //stop everything here
        }
    }

    public void SetHealthStats(float maxHealth, float healthRegen, float increaseAmount)
    {
        _maxHealth += maxHealth;
        _healthRegen = healthRegen;
        _currentHealth += increaseAmount;
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        Debug.Log("Current health: " + _currentHealth + "max health: " + _maxHealth);
        uiManager.SetProgress(_currentHealth / _maxHealth);
    }

    private void Die()
    {
        Debug.Log("died");
    }
}
