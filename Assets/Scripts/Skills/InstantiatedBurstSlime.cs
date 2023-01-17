using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatedBurstSlime : MonoBehaviour
{
    [Header("Burst Slime Stats")]
    [SerializeField] private float speed = 300f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float damage = 5f;

    public void ReleaseTheSlimes()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.forward * speed, ForceMode.Acceleration);
        Invoke("CloseTheSlime", 5);
    }

    private void CloseTheSlime()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            GameManager.Haptic(0);
            Debug.Log("Burst slime hit the enemy. Damage : " + damage);
            other.GetComponent<EnemyBase>().TakeDamage(damage);
        }
    }
}
