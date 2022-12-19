using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CamFollower : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private Vector3 offset;
    [SerializeField] private float playerFollowSpeed = 0.125f;
    //[SerializeField] private float clampLocalX = 1.5f;

    private void Start()
    {
        DOTween.Init();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = player.position + offset;
        //targetPosition.x = Mathf.Clamp(targetPosition.x, -clampLocalX, clampLocalX);
        transform.position = Vector3.Lerp(transform.position, targetPosition, playerFollowSpeed);
    }
}
