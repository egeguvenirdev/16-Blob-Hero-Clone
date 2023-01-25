using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static CardSelecter;

public abstract class SkillBase : MonoBehaviour
{
    [Header("Card Infos")]
    [SerializeField] protected string _skillName;
    [SerializeField] protected Sprite _cardImage;

    [Header("Odd Skill")]
    [SerializeField] protected string _oddSkillName;
    [SerializeField] protected float _skillOddValue;

    [Header("Even Skill")]
    [SerializeField] protected string _evenSkillName;
    [SerializeField] protected float _skillEvenValue;

    public string LevelKey => _skillName;
    public Sprite Image => _cardImage;
    public string[] SkillNames => new[] { _oddSkillName, _evenSkillName };
    public float[] SkillValues => new[] { _skillOddValue, _skillEvenValue };


    public abstract void Initialize();

    public abstract void DeInitialize();

    public void UpgradeButton()
    {
        int remainer = (PlayerPrefs.GetInt(_skillName, 0) + 1) % 2;
        if (remainer == 1)
        {
            OddLevelUpgrade();
        }
        else
        {
            EvenLevelUpgrade();
        }
        UpgradeLevelPref();
    }

    protected abstract void OddLevelUpgrade();

    protected abstract void EvenLevelUpgrade();

    public  void UpgradeLevelPref()
    {
        PlayerPrefs.SetInt(_skillName, PlayerPrefs.GetInt(_skillName, 0) + 1);
    }
}
