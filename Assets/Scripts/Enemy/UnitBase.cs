using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitBase : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 10;
    private float currentHealth;

    private void Start()
    {
        ResHealth();
    }

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

    protected virtual void Die()
    {
        ResHealth();
    }

    private void ResHealth()
    {
        currentHealth = maxHealth;
    }
}
