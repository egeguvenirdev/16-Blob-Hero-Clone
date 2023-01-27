using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerMover : MonoBehaviour
{
    [SerializeField] private Transform localMover;
    [SerializeField] private Transform ground;
    [SerializeField] private Transform player;

    [SerializeField] private float speed;
    [SerializeField] private VariableJoystick variableJoystick;

    public void Update()
    {
        Vector3 direction = (Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal) * speed;
        //localMover.position += new Vector3(direction.x, localMover.position.y, direction.z);
        localMover.LookAt(new Vector3(direction.x, localMover.position.y, direction.z)*-1);

        Vector3 nextPos = localMover.position + new Vector3(direction.x, localMover.position.y, direction.z) * Time.deltaTime;
        nextPos.x = Mathf.Clamp(nextPos.x, -495, 495);
        nextPos.z = Mathf.Clamp(nextPos.z, -495, 495);
        localMover.position = nextPos;
    }
}