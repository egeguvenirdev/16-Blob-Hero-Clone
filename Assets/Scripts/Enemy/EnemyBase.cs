using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : UnitBase
{
    [SerializeField] protected SimpleAnimancer _animancer;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected float damage;
    [SerializeField] private AnimationClip[] dieClips;
    protected PlayerManager playerManager;
    protected bool canRun = false;
    protected bool isRunning = false;
    protected bool isAlive = false;

    protected void Initialized()
    {
        playerManager = PlayerManager.Instance;
        isRunning = false;
    }

    protected virtual void OnEnable()
    {
        Initialized();
        AIManager.ManagerUpdate += MoveTowardsPlayer;
        AIManager.PlayerDied += OnGameEnd;
        isAlive = true;
    }

    protected virtual void OnDisable()
    {
        AIManager.ManagerUpdate -= MoveTowardsPlayer;
        isAlive = false;
    }

    protected virtual void MoveTowardsPlayer(Vector3 player)
    {
        //
    }

    public virtual void TakeDamage(float hitAmount)
    {
        setHealth = hitAmount;
    }

    private void OnGameEnd(bool playerWin)
    {
        if (playerWin)
        {
            OnPlayerWin();
            return;
        }
        OnPlayerLose();
    }

    private void OnPlayerWin()
    {
        if (isAlive)
        {
            int randomClipNumber = Random.Range(0, dieClips.Length);
            _animancer.PlayAnimation(dieClips[randomClipNumber]);
            agent.isStopped = true;
        }
    }

    private void OnPlayerLose()
    {
        if (isAlive)
        {
            _animancer.PlayAnimation("EnemyWin");
            agent.isStopped = true;
        }
    }
}
