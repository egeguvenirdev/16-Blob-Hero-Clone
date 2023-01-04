using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using Pathfinding;

public class AIManager : MonoBehaviour
{
    public static event Action<Transform> ManagerUpdate;
    [SerializeField] private Transform player;

    void FixedUpdate()
    {
        ManagerUpdate?.Invoke(player);
    }
}
