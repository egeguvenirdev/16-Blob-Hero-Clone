using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : EnemyBase
{
    [SerializeField] private SimpleAnimancer _animancer;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private bool canMove = true;
    private Vector3 destination;

    protected override void OnEnable()
    {
        base.OnEnable();
        EnemyDie += ResEnemy;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EnemyDie -= ResEnemy;
    }

    protected override void MoveTowardsPlayer(Vector3 player)
    {
        if (agent == null) return;

        if (player != null) // check the player if its dead or what
        {
            destination = player;
            agent.SetDestination(destination);
            //Debug.Log("dest: " + targetPos);
        }

        if (agent.remainingDistance < 1f)
        {
            if (canMove)
            {
                Debug.Log("punching");
                isRunning = false;
                _animancer.PlayAnimation("EnemyHit");
                StartCoroutine(HitRoutine());
            }
        }
        else
        {
            if (canMove && !isRunning)
            {
                Debug.Log("running");
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
        playerManager.setHealth = 5;
        yield return new WaitForSeconds(0.8f);
        _animancer.Stop();
        //isRunning = false;
        canMove = true;
    }

    protected override void Die()
    {
        ResEnemy();
        base.Die();
    }

    private void ResEnemy()
    {
        Debug.Log("enemyres");
        canMove = true;
        isRunning = false;
    }
}
