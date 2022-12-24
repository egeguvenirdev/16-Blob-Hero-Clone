using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class EnemyInstantiator : MonoBehaviour
{
    [SerializeField] private EnemyInstantiator _enemyInstantiator;

    [Header("Wave Settings")]
    [SerializeField] private int _waveCount;
    [SerializeField] private int _waveEnemyCount;
    [SerializeField] private int _waveCoolDown;

    [Header("Instantiate Settings")]
    [SerializeField] private Transform _groundTransform;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _circleRadius;
    [SerializeField] private GameObject _enemyPrefab;

    public void Initilalize()
    {
        _playerTransform = PlayerManager.Instance.transform;
    }

    [Button("CallEnemies")]
    public void CallEnemies()
    {
        CreateEnemiesAroundPoint(_waveEnemyCount, Vector3.zero, _circleRadius);
    }

    public void CreateEnemiesAroundPoint(int enemyCount, Vector3 point, float radius)
    {

        for (int i = 0; i < enemyCount; i++)
        {
            // Distance around the circle
            var radians = 2 * Mathf.PI / enemyCount * i;

            // direction
            var vertical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);

            var spawnDir = new Vector3(horizontal, 0, vertical);
            var spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

            var enemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            enemy.transform.LookAt(point);
        }
    }
}
