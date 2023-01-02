using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InstantiatedMeteor : MonoBehaviour
{
    [Header("Meteor Stats")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float targetPointValue = 0f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private Transform ground;
    [SerializeField] private Transform poolObject;

    private void Start()
    {
        poolObject = ObjectPooler.Instance.transform;
    }

    public void RainToEnemies()
    {
        ground = GameObject.FindGameObjectWithTag("Ground").transform;
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
        PlayParticle("Explosion");
        //transform.SetParent(poolObject);
        gameObject.SetActive(false);
    }

    private void PlayParticle(string particleName)
    {
        var particle = ObjectPooler.Instance.GetPooledObject(particleName);
        Vector3 particlePos = new Vector3(transform.position.x, 0.25f, transform.position.z);
        particle.transform.position = particlePos;
        particle.transform.rotation = Quaternion.identity;
        particle.SetActive(true);
        particle.transform.SetParent(ground);
        particle.GetComponent<ParticleSystem>().Play();
        //Invoke("SetActiveParticle", 2.5f);
    }
    
    private void SetActiveParticle(Transform particleRef)
    {
        particleRef.SetParent(poolObject);
    }
}
