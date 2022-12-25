using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoSingleton<UIManager>
{
    //Main UIs
    [SerializeField] private GameObject tapToPlayUI;
    [SerializeField] private GameObject nextLvMenuUI;
    [SerializeField] private GameObject restartLvUI;
    [SerializeField] private GameObject joystick;
    [Space]

    [Space]
    //status texts UIs
    [SerializeField] private TMP_Text currentLV;
    [SerializeField] private TMP_Text totalMoneyText;

    [Space]
    //status texts
    [SerializeField] private Image healthBarImage;

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
        if (!isPaused) //if the game not stopped
        {
            joystick.SetActive(false);
            tapToPlayUI.SetActive(false);
            nextLvMenuUI.SetActive(true);
            isPaused = true;
        }
    }

    public void RestartButtonUI()
    {
        if (!isPaused) //if the game not stopped
        {
            joystick.SetActive(false);
            restartLvUI.SetActive(true);
            isPaused = true;
        }
    }

    public void TapToPlayButton()
    {
        joystick.SetActive(true);
        tapToPlayUI.SetActive(false);
        isPaused = false;
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

    public void SetProgress(float health)
    {
        healthBarImage.fillAmount = health;
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
