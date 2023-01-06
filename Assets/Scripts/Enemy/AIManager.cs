using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using Pathfinding;

public class AIManager : MonoBehaviour
{
    public static event Action<Vector3> ManagerUpdate;
    [SerializeField] private Transform player;
    private int diedBossCount = 0;

    void FixedUpdate()
    {
        ManagerUpdate?.Invoke(player.position);
    }

    private void GameEnded()
    {
        //kill the sc
    }

    public void BossDied()
    {
        diedBossCount++;
    }
}
