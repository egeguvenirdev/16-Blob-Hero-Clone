using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatedTurningSlime : MonoBehaviour
{
    [Header("Turning Slime Stats")]
    [SerializeField] private float damage = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameManager.Haptic(0);
            Debug.Log("Burst slime hit the enemy. Damage : " + damage);
            other.GetComponent<EnemyBase>().TakeDamage(damage);
        }
    }
}
