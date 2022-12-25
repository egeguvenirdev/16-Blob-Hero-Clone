using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    [Header("Scripts")]
    [SerializeField] private RunnerScript _runnerScript;
    [SerializeField] private GameObject _healthBar;

    [Header("Player Stats")]
    [SerializeField] private float _maxHealth;
    private float _currentHealth;

    private bool canRun = false;
    Sequence sequence;

    void Start()
    {
        DOTween.Init();
        _currentHealth = _maxHealth;
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
            UIManager.Instance.SetProgress(0);
            Die();
        }
        else
        {
            _currentHealth -= dealedDamage;
            UIManager.Instance.SetProgress(_currentHealth / _maxHealth);
        }
    }

    private void Die()
    {

    }
}
