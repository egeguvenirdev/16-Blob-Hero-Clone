using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    private float health = 10;
    private Transform ground;

    private void Start()
    {
        ground = GameObject.FindGameObjectWithTag("Ground").transform;
    }

    public float currentHealth
    {
        get => health;
        set
        {
            value = Mathf.Clamp(value, 0, float.MaxValue);
            currentHealth -= value;

            if (health <= 0) Die();
        }
    }

    private void Die()
    {
        PlayParticle();
        DropExpDiamond();
        ResTheEnemy();
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
        diamond.transform.SetParent(ground);
    }

    private void ResTheEnemy()
    {
        currentHealth = health;
    }
}
