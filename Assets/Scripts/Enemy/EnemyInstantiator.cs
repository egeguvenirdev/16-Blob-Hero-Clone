using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class EnemyInstantiator : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] private int waveCount;
    [SerializeField] private int waveEnemyCount;
    [SerializeField] private int bossCount = 1;
    [SerializeField] private int waveCoolDown;
    [SerializeField] private bool canInstantiate = true;

    [Header("Instantiate Settings")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _circleRadius;
    [SerializeField] private GameObject _enemyPrefab;
    private ObjectPooler objectPooler;
    private HcLevelManager levelManager;
    private PlayerManager playerManager;
    private bool canSpawnBoss = true;

    [SerializeField]
    private float _waveEnemyCount
    {
        get => waveEnemyCount;
        set
        {
            value = Mathf.Clamp(value, waveEnemyCount, 20);
            waveEnemyCount = (int)value;
        }
    }

    [SerializeField]
    private float _waveCoolDown
    {
        get => waveCoolDown;
        set
        {
            value = Mathf.Clamp(value, waveCoolDown, 10);
            waveCoolDown = (int)value;
        }
    }

    public void Init()
    {
        playerManager = PlayerManager.Instance;
        objectPooler = ObjectPooler.Instance;
        levelManager = HcLevelManager.Instance;
        int level = levelManager.GetGlobalLevelIndex();
        _waveEnemyCount += level;
        _waveCoolDown -= level;
        bossCount = (int)Mathf.Clamp(level / 2, 1, 5);
        Invoke("InvokeEnemy", 0.5f);
    }

    public void DeInit()
    {
        StopAllCoroutines();
    }

    private void LateUpdate()
    {
        if (playerManager!=null)
        {
            int level = playerManager.GetLevel();
            if (level >= 10 && canSpawnBoss)
            {
                canSpawnBoss = false;
                CallRandomBoss();
            }
        }
    }

    private void InvokeEnemy()
    {
        StopAllCoroutines();
        StartCoroutine(CallEnemies());
    }

    public IEnumerator CallEnemies()
    {
        Debug.Log("Instantiate has been started. Enemy Count: " + waveEnemyCount + " Wave Cooldown: " + waveCoolDown);
        for (int i = 0; i < waveCount; i++)
        {
            Vector3 instantiatePos = playerManager.GetCharacterTransform().position;
            CreateEnemiesAroundPoint(waveEnemyCount, instantiatePos, _circleRadius);
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
        }
    }

    private void CallRandomBoss()
    {
        for (int i = 0; i < bossCount; i++)
        {
            // Distance around the circle
            var radians = 2 * Mathf.PI / bossCount * i;

            // direction
            var vertical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);

            var spawnDir = new Vector3(horizontal, 0, vertical);
            var spawnPos = playerManager.GetCharacterTransform().position + spawnDir * _circleRadius; // Radius is just the distance away from the point

            var enemy = objectPooler.GetPooledObject("Enemy");
            enemy.transform.position = spawnPos;
            enemy.SetActive(true);
            enemy.transform.LookAt(playerManager.GetCharacterTransform());
        }
    }

    private void GameEnded()
    {
        //kill the sc
    }
}
