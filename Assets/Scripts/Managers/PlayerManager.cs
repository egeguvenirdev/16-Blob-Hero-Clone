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

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (canRun)
        {
            runnerScript.StartToRun(true);
        }
    }

    public void StopMovement()
    {
        runnerScript.StartToRun(false);
        canRun = false;
    }

    /*public void SwitchPath()
    {
        runnerScript.SwitchPathLine();
    }*/
}
