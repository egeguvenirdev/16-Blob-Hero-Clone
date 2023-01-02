using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;
public class GameManager : MonoSingleton<GameManager>
{
    [Header("PlayerPrefs")]
    [SerializeField] private bool clearPlayerPrefs;

    [Header("Money Settings")]
    [SerializeField] private int addMoney = 0;

    private int totalMoney;
    private EnemyInstantiator enemyInstantiator;

    void Start()
    {
        if (clearPlayerPrefs)
        {
            PlayerPrefs.DeleteAll();
            SetTotalMoney(addMoney);
        }
        enemyInstantiator = FindObjectOfType<EnemyInstantiator>();

        PlayerManager.Instance.Init();
        SkillManager.Instance.Init();
        enemyInstantiator.Init();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetTotalMoney(1000);
        }
    }

    public void SetTotalMoney(int collectedAmount)
    {
        totalMoney = PlayerPrefs.GetInt("TotalMoney", 0) + collectedAmount;
        PlayerPrefs.SetInt("TotalMoney", totalMoney);
        UIManager.Instance.SetMoneyUI(totalMoney, true);

        totalMoney = 0;
    }


    public static void Haptic(int type)
    {
        if (type == 0)
        {
            MMVibrationManager.Haptic(HapticTypes.LightImpact);
        }
        else if (type == 1)
        {
            MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        }
        else if (type == 2)
        {
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        }
    }
}
