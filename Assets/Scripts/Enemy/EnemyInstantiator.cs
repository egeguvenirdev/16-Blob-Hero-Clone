using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class EnemyInstantiator : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] private int waveCount;
    [SerializeField] private int waveEnemyCount;
    [SerializeField] private int waveCoolDown;
    [SerializeField] private bool canInstantiate = true;

    [Header("Instantiate Settings")]
    [SerializeField] private Transform groundTransform;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _circleRadius;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private ObjectPooler objectPooler;
    [SerializeField] private HcLevelManager levelManager;

    [SerializeField] private float _waveEnemyCount
    {
        get => waveEnemyCount;
        set
        {
            value = Mathf.Clamp(value, 10, 20);
            waveEnemyCount = (int)value;
        }
    }

    [SerializeField]private float _waveCoolDown
    {
        get => waveCoolDown;
        set
        {
            value = Mathf.Clamp(value, 5, 10);
            waveCoolDown = (int)value;
        }
    }

    public void Init()
    {
        int level = HcLevelManager.Instance.GetGlobalLevelIndex();
        _playerTransform = PlayerManager.Instance.transform;
        objectPooler = ObjectPooler.Instance;
        groundTransform = GameObject.FindGameObjectWithTag("Ground").transform;
        _waveEnemyCount += level;
        _waveCoolDown -= level;
        StartCoroutine(CallEnemies());
    }

    public IEnumerator CallEnemies()
    {
        Debug.Log("Instantiate has been started. Enemy Count: " + waveEnemyCount + " Wave Cooldown: " + waveCoolDown);
        while (canInstantiate)
        {
            CreateEnemiesAroundPoint(waveEnemyCount, Vector3.zero, _circleRadius);
            yield return new WaitForSeconds(waveCoolDown);
        }
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

            var enemy = objectPooler.GetPooledObject("Enemy");
            enemy.transform.position = spawnPos;
            enemy.SetActive(true);
            enemy.transform.LookAt(point);
            enemy.transform.parent = groundTransform;
        }
    }
}
