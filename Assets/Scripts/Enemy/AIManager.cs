using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using Pathfinding;

public class AIManager : MonoBehaviour
{
    public static event Action<Vector3> ManagerUpdate;
    [SerializeField] private Transform player;

    void Start()
    {
        
    }


    void FixedUpdate()
    {
        ManagerUpdate?.Invoke(Vector3.zero);
    }
}
