using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InstantiatedMeteor : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float targetPointValue = -5f;
    [SerializeField] private float damage = 5f;

    void Start()
    {
        RainToEnemies();
    }

    private void RainToEnemies()
    {
        transform.DOMoveY(targetPointValue, speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //
            other.GetComponent<EnemyBase>().TakeDamage(damage);
        }

        if (other.CompareTag("Ground"))
        {
            var particle = ObjectPooler.Instance.GetPooledObject("HitParticle");
            Vector3 particlePos = new Vector3(other.transform.position.x, 0.25f, other.transform.position.z);
            particle.transform.position = particlePos;
            particle.transform.rotation = Quaternion.identity;
            particle.SetActive(true);
            particle.GetComponent<ParticleSystem>().Play();
        }
    }
}
