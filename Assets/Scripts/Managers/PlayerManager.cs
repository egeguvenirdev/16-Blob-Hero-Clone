using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    [Header("Scripts")]
    [SerializeField] private RunnerScript runnerScript;

    private bool canRun = false;
    Sequence sequence;

    void Start()
    {
        DOTween.Init();
    }

    public void StartMovement()
    {
        runnerScript.StartToRun(true);
        canRun = true;
    }

    public void StopMovement()
    {
        runnerScript.StartToRun(false);
        canRun = false;
    }
}
