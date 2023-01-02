using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatedBurstSlime : MonoBehaviour
{
    [Header("Burst Slime Stats")]
    [SerializeField] private float speed = 300f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform ground;
    private Transform poolObject;

    void Start()
    {
        poolObject = ObjectPooler.Instance.transform;
        ground = GameObject.FindGameObjectWithTag("Ground").transform;
    }

    public void ReleaseTheSlimes()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.forward * speed, ForceMode.Acceleration);
        transform.SetParent(ground);
        Invoke("CloseTheSlime", 5);
    }

    private void CloseTheSlime()
    {
        transform.SetParent(poolObject);
        gameObject.SetActive(false);
    }
}
