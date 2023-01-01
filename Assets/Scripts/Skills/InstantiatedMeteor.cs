using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InstantiatedMeteor : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float targetPointValue = 0f;
    [SerializeField] private float damage = 5f;

    public void RainToEnemies()
    {
        transform.DOLocalMoveY(targetPointValue, speed).OnComplete( ()=> { Explode(); } );

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBase>().TakeDamage(damage);
        }
    }

    private void Explode()
    {
        var particle = ObjectPooler.Instance.GetPooledObject("Explosion");
        Vector3 particlePos = new Vector3(transform.position.x, 0.25f, transform.position.z);
        particle.transform.position = particlePos;
        particle.transform.rotation = Quaternion.identity;
        particle.SetActive(true);
        particle.GetComponent<ParticleSystem>().Play();
        gameObject.SetActive(false);
    }
}
