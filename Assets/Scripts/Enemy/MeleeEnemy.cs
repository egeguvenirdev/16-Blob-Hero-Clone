using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class MeleeEnemy : EnemyBase
{
    [SerializeField] private SimpleAnimancer _animancer;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private bool canMove = true;
    private Vector3 destination;

    protected override void OnEnable()
    {
        base.OnEnable();
        ResEnemy();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void MoveTowardsPlayer(Vector3 player)
    {
        if (agent == null) return;

        if (player != null) // check the player if its dead or what
        {
            destination = player;
            agent.SetDestination(destination);
            //Debug.Log(agent.remainingDistance);
        }

        if (agent.remainingDistance > 0 && agent.remainingDistance < 1f)
        {
            if (canMove)
            {
                isRunning = false;
                _animancer.PlayAnimation("EnemyHit");
                StartCoroutine(HitRoutine());
            }
        }
        else
        {
            if (canMove && !isRunning)
            {
                _animancer.PlayAnimation("EnemyRun");
                StopAllCoroutines();
                isRunning = true;
            }
        }
    }

    private IEnumerator HitRoutine()
    {
        canMove = false;
        yield return new WaitForSeconds(0.3f);
        GameManager.Haptic(1);
        playerManager.setHealth = 5;
        yield return new WaitForSeconds(0.8f);
        _animancer.Stop();
        //isRunning = false;
        canMove = true;
    }

    protected override void Die()
    {
        ResEnemy();
        PlayParticle();
        DropExpDiamond();   
        base.Die();
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
        diamond.transform.DOJump(diamond.transform.position, 2, 1, 0.3f).OnComplete( ()=> 
        {
            diamond.GetComponent<Collider>().enabled = true;
        } );
    }

    private void ResEnemy()
    {
        Debug.Log("enemyres");
        canMove = true;
        isRunning = false;
    }
}
