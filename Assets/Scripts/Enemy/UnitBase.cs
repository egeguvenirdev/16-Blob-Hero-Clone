using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitBase : MonoBehaviour
{
    public event Action EnemyDie;
    [SerializeField] private float maxHealth = 10;
    private float currentHealth;

    private void Start()
    {
        ResTheEnemy();
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

    private void Die()
    {
        PlayParticle();
        DropExpDiamond();
        ResTheEnemy();
        EnemyDie?.Invoke();
        gameObject.SetActive(false);
    }

    private void PlayParticle()
    {
        var particle = ObjectPooler.Instance.GetPooledObject("HitParticle");
        Vector3 particlePos = new Vector3(transform.position.x, 0.25f, transform.position.z);
        particle.transform.position = particlePos;
        particle.transform.rotation = Quaternion.identity;
        particle.SetActive(true);
        particle.GetComponent<ParticleSystem>().Play();
    }

    private void DropExpDiamond()
    {
        GameObject diamond = ObjectPooler.Instance.GetPooledObject("Gem");
        diamond.transform.position = transform.position;
        diamond.transform.rotation = Quaternion.identity;
        diamond.SetActive(true);
    }

    private void ResTheEnemy()
    {
        currentHealth = maxHealth;
    }
}
