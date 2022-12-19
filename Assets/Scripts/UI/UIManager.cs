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
    [Space]

    [Space]
    //status texts UIs
    [SerializeField] private TMP_Text currentLV;
    [SerializeField] private TMP_Text totalMoneyText;

    [Space]
    //status texts
    [SerializeField] private Image progressBarImage;

    public bool isPaused;
    private int smoothMoneyNumbers = 0;
    private Tweener smoothTween;

    private void Start()
    {
        isPaused = true;
        DOTween.Init();
        LevelText();
    }

    public void TapToPlayButton()
    {
        tapToPlayUI.SetActive(false);
        isPaused = false;
    }

    public void NextLvUI()
    {
        if (!isPaused) //if the game not stopped
        {
            tapToPlayUI.SetActive(false);
            nextLvMenuUI.SetActive(true);
            isPaused = true;
        }
    }

    public void RestartButtonUI()
    {
        if (!isPaused) //if the game not stopped
        {
            restartLvUI.SetActive(true);
            isPaused = true;
        }
    }

    public void PauseButtonUI()
    {
        if (!isPaused) //if the game not stopped
        {
            tapToPlayUI.SetActive(true);
            isPaused = true;
        }
    }

    public void NextLevelButton()
    {
        nextLvMenuUI.SetActive(false);
        isPaused = false;
        HcLevelManager.Instance.LevelUp();
        LevelText();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //SAHNEYI YUKLE BASTAN
    }

    public void RestartLevelButton()
    {
        restartLvUI.SetActive(false);
        isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelText()
    {
        int levelInt = HcLevelManager.Instance.GetGlobalLevelIndex() + 1;
        currentLV.text = "Level " + levelInt;
    }

    public void SetProgress(float progress)
    {
        progressBarImage.fillAmount = progress;
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
