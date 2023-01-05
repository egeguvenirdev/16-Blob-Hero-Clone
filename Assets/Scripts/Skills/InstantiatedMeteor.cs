using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InstantiatedMeteor : MonoBehaviour
{
    [Header("Meteor Stats")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float diameter = 2f;
    [SerializeField] private float targetPointValue = 0f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private Transform poolObject;
    [SerializeField] private LayerMask layer;

    private void Start()
    {
        poolObject = ObjectPooler.Instance.transform;
    }

    public void RainToEnemies()
    {
        transform.DOLocalMoveY(targetPointValue, speed).OnComplete(() => { Explode(); });
    }

    private void Explode()
    {
        GameManager.Haptic(0);
        PlayParticle("Explosion");
        DetectEnemies();
        gameObject.SetActive(false);
    }

    private void PlayParticle(string particleName)
    {
        var particle = ObjectPooler.Instance.GetPooledObject(particleName);
        Vector3 particlePos = new Vector3(transform.position.x, 0.25f, transform.position.z);
        particle.transform.position = particlePos;
        particle.transform.rotation = Quaternion.identity;
        particle.SetActive(true);
        particle.GetComponent<ParticleSystem>().Play();
    }

    private void DetectEnemies()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, diameter, layer);

        foreach (Collider gems in collider)
        {
            gems.GetComponent<EnemyBase>().TakeDamage(damage);
            Debug.Log("Meteor hit the enemy. Damage : " + damage);
        }
    }
}
