using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : EnemyBase
{
    [SerializeField] private SimpleAnimancer _animancer;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private bool canMove = true;

    protected override void MoveTowardsPlayer(Transform player)
    {
        if (agent == null) return;

        if (player != null) // check the player if its dead or what
        {
            Vector3 targetPos =player.position;
            targetPos += (transform.position - player.position).normalized * 1;
            agent.SetDestination(targetPos);
            //Debug.Log("dest: " + targetPos);
        }

        //Debug.Log("remanining dist: " + agent.remainingDistance);

        if (agent.remainingDistance > 0.1f)
        {
            if (!isRunning)
            {
                _animancer.PlayAnimation("EnemyRun");
                StopAllCoroutines();
                isRunning = true;
            }
        }
        else
        {
            if (canMove)
            {
                _animancer.PlayAnimation("EnemyHit");
                StartCoroutine(HitRoutine());
            }
        }
    }

    private IEnumerator HitRoutine()
    {
        canMove = false;
        isRunning = false;
        yield return new WaitForSeconds(0.3f);
        playerManager.setHealth = 5;
        yield return new WaitForSeconds(0.8f);
        _animancer.Stop();
        canMove = true;
    }
}
