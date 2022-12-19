using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using DG.Tweening;

public class RunnerScript : MonoBehaviour
{
    [Header("Scripts and Transforms")]
    [SerializeField] private Transform model;
    [SerializeField] private Transform localMoverTarget;
    [SerializeField] private SimpleAnimancer animancer;
    [SerializeField] private PlayerSwerve playerSwerve;
    
    [Header("Run Settings")]
    [SerializeField] private float swipeLerpSpeed = 2f;

    private Vector3 oldPosition;
    private bool canSwerve = false;
    private bool canFollow = true;
    private string currentAnimName = "Walking";

    void Awake()
    {
        playerSwerve.OnSwerveStart += PlayerSwipe_OnPointerDown;
        playerSwerve.OnSwerveEnd += PlayerSwipe_OnPointerUp;
        oldPosition = localMoverTarget.localPosition;
    }

    public void Init()
    {
        //PlayAnimation("StartIdle");
    }

    void Update()
    {
        FollowLocalMoverTarget();
        oldPosition = model.localPosition;
    }

    public void StartToRun(bool checkRun)
    {
        if (checkRun)
        {
            canSwerve = true;
            canFollow = true;
        }
        else
        {
            canSwerve = false;
        }
    }

    private void PlayerSwipe_OnPointerDown()
    {
        PlayAnimation("Running");
    }

    private void PlayerSwipe_OnPointerUp()
    {
        PlayAnimation("Idle");
    }

    void FollowLocalMoverTarget()
    {
        if (canFollow)
        {
            //Vector3 direction = localMoverTarget.localPosition - oldPosition;
            //animancer.GetAnimatorTransform().forward = Vector3.Lerp(animancer.GetAnimatorTransform().forward, direction, swipeRotateLerpSpeed * Time.deltaTime);

            //swipe the object
            //Vector3 nextPos = new Vector3(localMoverTarget.localPosition.x, model.localPosition.y, model.localPosition.z); ;
            model.localPosition = Vector3.Lerp(model.localPosition, localMoverTarget.localPosition, swipeLerpSpeed * Time.deltaTime);
        }
    }

    public void PlayAnimation(string animName)
    {
        animancer.PlayAnimation(animName);

        currentAnimName = animName;
    }

    public void PlayAnimation(string animName, float speed)
    {
        animancer.PlayAnimation(animName);
        animancer.SetStateSpeed(speed);
        currentAnimName = animName;
    }

    public void CanSwerve()
    {
        canSwerve = false;
    }

    public void StopMovement()
    {
        //canFollow = false;
        canSwerve = false;
    }
}
