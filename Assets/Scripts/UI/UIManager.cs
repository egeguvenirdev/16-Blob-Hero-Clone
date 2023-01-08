using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using NaughtyAttributes;
public class UIManager : MonoSingleton<UIManager>
{
    [Header("Panels")]
    [SerializeField] private GameObject tapToPlayUI;
    [SerializeField] private GameObject nextLvMenuUI;
    [SerializeField] private GameObject restartLvUI;
    [SerializeField] private GameObject cards;
    [SerializeField] private GameObject joystick;

    [Header("Level & Money")]
    [SerializeField] private TMP_Text currentLV;
    [SerializeField] private TMP_Text totalMoneyText;

    [Header("Health & Xp Bars")]
    [SerializeField] private Image healthBarImage;
    [SerializeField] private GameObject xpBar;
    [SerializeField] private Image xpBarImage;

    [Header("Scripts")]
    [SerializeField] private CardSelecter cardSelecter;

    public bool isPaused;
    private int smoothMoneyNumbers = 0;
    private Tweener smoothTween;

    private void Start()
    {
        isPaused = true;
        DOTween.Init();
        LevelText();
    }


    public void NextLvUI()
    {
        if (!isPaused)
        {
            joystick.SetActive(false);
            tapToPlayUI.SetActive(false);
            nextLvMenuUI.SetActive(true);
            isPaused = true;
        }
    }

    public void RestartButtonUI()
    {
        if (!isPaused)
        {
            joystick.SetActive(false);
            restartLvUI.SetActive(true);
            isPaused = true;
        }
    }

    public void TapToPlayButton()
    {
        OpenUpgradeCardPanel();
        tapToPlayUI.SetActive(false);
        xpBar.SetActive(true);
        isPaused = false;
        GameManager.Instance.ReleaseTheEnemies();
        PlayerManager.Instance.StartMovement();
    }

    public void NextLevelButton()
    {
        joystick.SetActive(true);
        nextLvMenuUI.SetActive(false);
        isPaused = false;
        HcLevelManager.Instance.LevelUp();
        LevelText();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //SAHNEYI YUKLE BASTAN
    }

    public void RestartLevelButton()
    {
        joystick.SetActive(true);
        restartLvUI.SetActive(false);
        isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelText()
    {
        int levelInt = HcLevelManager.Instance.GetGlobalLevelIndex() + 1;
        currentLV.text = "Level " + levelInt;
    }

    public void SetPlayerHealth(float health)
    {
        healthBarImage.fillAmount = health;
    }

    public void SetPlayerXp(float xp)
    {
        xpBarImage.fillAmount = xp;
    }

    public void SetMoneyUI(int totalMoney, bool setSmoothly)
    {
        //totalMoneyText.text = money;

        if (setSmoothly)
        {
            smoothTween.Kill();
            smoothTween = DOTween.To(() => smoothMoneyNumbers, x => smoothMoneyNumbers = x, totalMoney, 0.5f).SetSpeedBased(false).OnUpdate(() => { UpdateMoneyText(); });
        }
        else
        {
            smoothTween.Kill();
            smoothMoneyNumbers = totalMoney;
            UpdateMoneyText();
        }
    }

    private void UpdateMoneyText()
    {
        totalMoneyText.text = FormatFloatToReadableString(smoothMoneyNumbers);
    }

    [Button]
    public void OpenUpgradeCardPanel()
    {
        joystick.SetActive(false);
        cardSelecter.FillCardsInfos();
        cards.SetActive(true);
        TimeManager.StopTime();
    }

    public void CloseUpgradeCardPanel()
    {
        joystick.SetActive(true);
        cards.SetActive(false);
        TimeManager.StartTime();
    }

    public string FormatFloatToReadableString(float value)
    {
        float number = value;
        if (number < 1000)
        {
            return ((int)number).ToString();
        }
        string result = number.ToString();

        if (result.Contains(","))
        {
            result = result.Substring(0, 4);
            result = result.Replace(",", string.Empty);
        }
        else
        {
            result = result.Substring(0, 3);
        }

        do
        {
            number /= 1000;
        }
        while (number >= 1000);
        number = Mathf.CeilToInt(number);
        if (value >= 1000000000000000)
        {
            result = result + "Q";
        }
        else if (value >= 1000000000000)
        {
            result = result + "T";
        }
        else if (value >= 1000000000)
        {
            result = result + "B";
        }
        else if (value >= 1000000)
        {
            result = result + "M";
        }
        else if (value >= 1000)
        {
            result = result + "K";
        }

        if (((int)number).ToString().Length > 0 && ((int)number).ToString().Length < 3)
        {
            result = result.Insert(((int)number).ToString().Length, ".");
        }
        return result;
    }
}
