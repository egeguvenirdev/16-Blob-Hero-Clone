using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : UnitBase
{
    protected virtual void MoveTowardsPlayer(Vector3 player)
    {
        //
    }

    protected virtual void AttackToPlayer()
    {
        //
    }

    public void TakeDamage(float hitAmount)
    {
        health -= hitAmount;
    }
}
