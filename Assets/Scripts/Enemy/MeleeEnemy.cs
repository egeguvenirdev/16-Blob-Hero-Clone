using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MeleeEnemy : EnemyBase
{
    private AIDestinationSetter aiDestination;
    IAstarAI ai;

    private void OnEnable()
    {
        AIManager.ManagerUpdate += MoveTowardsPlayer;
        ai = GetComponent<IAstarAI>();
    }

    private void OnDisable()
    {
        AIManager.ManagerUpdate -= MoveTowardsPlayer;
    }

    protected override void MoveTowardsPlayer(Vector3 player)
    {
        if (ai == null) return;

        if(player != null) // check the player if its dead or what
        {
            Vector3 targetPos = (transform.position - player).normalized * -1;
            ai.destination = player - targetPos;
        }
    }
}
