using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GemExp : MonoBehaviour
{
    [SerializeField] private int speed = 2;

    private void OnEnable()
    {
        StartRotate();
    }

    private void StartRotate()
    {
        transform.DORotate(Vector3.up * 360, 2, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }
}
