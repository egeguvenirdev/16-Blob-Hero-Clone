using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiator : MonoBehaviour
{
    [SerializeField] private EnemyInstantiator enemyInstantiator;
    [SerializeField] private int waveCount;
    [SerializeField] private int waveCoolDown;

    [Header("Instantiate Settings")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float circleRadius;
    [SerializeField] private GameObject enemyPrefab;

    public void Init()
    {

    }

    // Update is called once per frame
    void Update()
    {

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

            var enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            enemy.transform.LookAt(point);
        }
    }
}
