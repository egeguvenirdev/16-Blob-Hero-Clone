using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerMover : MonoBehaviour
{
    [SerializeField] private Transform localMover;
    [SerializeField] private Transform player;

    [SerializeField] private float speed;
    [SerializeField] private VariableJoystick variableJoystick;

    public void FixedUpdate()
    {
        Vector3 direction = (Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal) * speed;
        localMover.position += new Vector3(direction.x, localMover.position.y, direction.z);
    }
}