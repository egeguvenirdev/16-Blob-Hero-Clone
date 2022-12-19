using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;
public class GameManager : MonoSingleton<GameManager>
{
    [Header("Ground Color Settings")]
    [SerializeField] private Material mat;
    [SerializeField] private Color32[] colorTypes = { };
    [SerializeField] private Material[] skyboxes = { };

    private int totalMoney;

    void Start()
    {

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

    private void SetGroundColor()
    {
        mat.SetColor("_HColor", colorTypes[Random.Range(0,5)]);
        RenderSettings.skybox = skyboxes[Random.Range(0, 5)];
    }
}
