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

    private void OnEnable()
    {
        Initialized();
        AIManager.ManagerUpdate += MoveTowardsPlayer;
        //ai = GetComponent<IAstarAI>();
    }

    private void OnDisable()
    {
        AIManager.ManagerUpdate -= MoveTowardsPlayer;
    }

    protected virtual void MoveTowardsPlayer(Transform player)
    {
        //
    }

    public void TakeDamage(float hitAmount)
    {
        currentHealth = hitAmount;
    }
}
