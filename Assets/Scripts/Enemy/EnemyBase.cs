using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class EnemyBase : UnitBase
{
    [SerializeField] protected float damage;
    protected AIDestinationSetter _aiDestination;
    protected PlayerManager playerManager;
    protected bool _canRun = false;
    protected bool _isRunning = false;
    protected IAstarAI ai;

    protected void Initialized()
    {
        playerManager = PlayerManager.Instance;
    }

    private void OnEnable()
    {
        Initialized();
        AIManager.ManagerUpdate += MoveTowardsPlayer;
        ai = GetComponent<IAstarAI>();
    }

    private void OnDisable()
    {
        AIManager.ManagerUpdate -= MoveTowardsPlayer;
    }

    protected virtual void MoveTowardsPlayer(Vector3 player)
    {
        //
    }

    public void TakeDamage(float hitAmount)
    {
        health -= hitAmount;
    }
}
