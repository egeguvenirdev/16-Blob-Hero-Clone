using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    private float _health = 100f;

    public float health
    {
        get => _health;
        set
        {
            value = Mathf.Clamp(value, 0, float.MaxValue);
            _health = value;

            if (_health <= 0) Die();
        }
    }

    private void Die()
    {
        //
    }
}
