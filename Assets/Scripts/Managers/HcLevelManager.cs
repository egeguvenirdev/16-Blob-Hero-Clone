using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HcLevelManager : MonoSingleton<HcLevelManager>
{

    [SerializeField] private GameObject[] levelPrefabs;
    [SerializeField] private int levelIndex = 0;
    [SerializeField] private bool forceLevel = false;

    private int _globalLevelIndex = 0;
    private bool _inited = false;
    private GameObject _currentLevel;

    public void Init()
    {
        if (_inited)
        {
            return;
        }
        _inited = true;
        _globalLevelIndex = PlayerPrefs.GetInt("HCLevel");

        if (forceLevel)
        {
            _globalLevelIndex = levelIndex;
            return;
        }
        levelIndex = _globalLevelIndex;

        if (levelIndex >= levelPrefabs.Length)
        {
            levelIndex = Random.Range(0, levelPrefabs.Length);
        }
    }

    private void Awake()
    {
        Init();
        GenerateCurrentLevel();
    }

    public void GenerateCurrentLevel()
    {
        if (_currentLevel != null)
        {
            Destroy(_currentLevel);
        }
        _currentLevel = Instantiate(levelPrefabs[levelIndex]);
    }

    public GameObject GetCurrentLevel()
    {
        return _currentLevel;
    }

    public void LevelUp()
    {
        if (forceLevel)
        {
            return;
        }

        _globalLevelIndex++;
        PlayerPrefs.SetInt("HCLevel", _globalLevelIndex);
        levelIndex = _globalLevelIndex;

        if (levelIndex >= levelPrefabs.Length)
        {
            levelIndex = Random.Range(0, levelPrefabs.Length);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public int GetGlobalLevelIndex()
    {
        return _globalLevelIndex;
    }
}
