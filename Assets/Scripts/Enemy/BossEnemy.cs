using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : EnemyBase
{
    [SerializeField] private SimpleAnimancer _animancer;
    [SerializeField] private AIManager aiManager;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private bool canMove = true;
    private Vector3 destination;

    private void OnEnable()
    {
        EnemyDie += OnBossDied;
    }

    private void OnDisable()
    {
        EnemyDie -= OnBossDied;
    }

    void Start()
    {
        aiManager = FindObjectOfType<AIManager>();
        maxHealth = 100;
    }

    protected override void MoveTowardsPlayer(Vector3 player)
    {
        if (agent == null) return;

        if (player != null) // check the player if its dead or what
        {
            destination = player;
            agent.SetDestination(destination);
        }

        if (agent.remainingDistance < 2f)
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
        canMove = true;
    }

    private void OnBossDied()
    {
        aiManager.BossDied();
        Destroy(gameObject);
    }
}
