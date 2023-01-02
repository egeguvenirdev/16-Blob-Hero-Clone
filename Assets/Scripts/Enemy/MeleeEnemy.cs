using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyBase
{
    [SerializeField] private SimpleAnimancer _animancer;

    protected override void MoveTowardsPlayer(Vector3 player)
    {
        if (ai == null) return;

        //Debug.Log(ai.remainingDistance);

        if (ai.canMove)
        {
            if (player != null) // check the player if its dead or what
            {
                Vector3 targetPos = (transform.position - player).normalized * -1;
                ai.destination = player - targetPos;
            }

            if (ai.remainingDistance > 0.1f)
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
                _animancer.PlayAnimation("EnemyHit");
                StartCoroutine(HitRoutine());
            }
        }
    }

    private IEnumerator HitRoutine()
    {
        ai.canMove = false;
        isRunning = false;
        yield return new WaitForSeconds(0.3f);
        playerManager.setHealth = 5;
        yield return new WaitForSeconds(0.8f);
        _animancer.Stop();
        ai.canMove = true;
    }
}
