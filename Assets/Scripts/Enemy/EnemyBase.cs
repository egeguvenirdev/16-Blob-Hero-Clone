using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : UnitBase
{
    [SerializeField] protected float damage;
    protected PlayerManager playerManager;
    protected bool canRun = false;
    protected bool isRunning = false;

    protected void Initialized()
    {
        playerManager = PlayerManager.Instance;
    }

    protected virtual void OnEnable()
    {
        Initialized();
        AIManager.ManagerUpdate += MoveTowardsPlayer;
        //ai = GetComponent<IAstarAI>();
    }

    protected virtual void OnDisable()
    {
        AIManager.ManagerUpdate -= MoveTowardsPlayer;
    }

    protected virtual void MoveTowardsPlayer(Vector3 player)
    {
        //
    }

    public void TakeDamage(float hitAmount)
    {
        setHealth = hitAmount;
    }
}
