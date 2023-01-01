using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    private float _health = 100f;

    public float health
    {
        get => _health;
        set
        {
            value = Mathf.Clamp(value, 0, float.MaxValue);
            _health = value;

            if (_health <= 0) Die();
        }
    }

    private void Die()
    {
        var particle = ObjectPooler.Instance.GetPooledObject("Blood");
        Vector3 particlePos = new Vector3(transform.position.x, 0.25f, transform.position.z);
        particle.transform.position = particlePos;
        particle.transform.rotation = Quaternion.identity;
        particle.SetActive(true);
        particle.GetComponent<ParticleSystem>().Play();
        gameObject.SetActive(false);
        ResTheEnemy();
    }

    private void ResTheEnemy()
    {
        health = _health;
    }
}
