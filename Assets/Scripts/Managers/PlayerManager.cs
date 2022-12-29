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

    public void Init()
    {
        DOTween.Init();
        _currentHealth = _maxHealth;
        _currentXP = _levelupReqXP;
        uiManager = UIManager.Instance;
    }

    private void FixedUpdate()
    {
        _currentHealth += _healthRegen;
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

    public void TakeDamage(float dealedDamage)
    {
        if ((_currentHealth - dealedDamage) <= 0)
        {
            uiManager.SetProgress(0);
            Die();
        }
        else
        {
            _currentHealth -= dealedDamage;
            uiManager.SetProgress(_currentHealth / _maxHealth);
        }
    }

    public void UpdateHealth()
    {
        uiManager.SetProgress(_currentHealth / _maxHealth);
    }

    public void GainXP()
    {
        if (_currentXP > _levelupReqXP)
        {
            uiManager.OpenUpgradeCardPanel();
            //stop everything here
        }
    }

    public void SetHealthStats(float maxHealth, float healthRegen)
    {
        _maxHealth = maxHealth;
        _healthRegen = healthRegen;
        UpdateHealth();
    }

    private void Die()
    {

    }
}
