using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class SkillBase : MonoBehaviour
{
    [Header("Card Infos")]    
    [SerializeField] protected string _skillName;
    [SerializeField] protected int _skillLevel;
    [SerializeField] protected Sprite _cardImage;

    [Header("Odd Skill")]
    [SerializeField] protected string _oddSkillName;
    [SerializeField] protected float _skillOddValue;

    [Header("Even Skill")]
    [SerializeField] protected string _evenSkillName;
    [SerializeField] protected float _skillEvenValue;


    public abstract void Initialize();

    public abstract void OddLevelUpgrade();

    public abstract void EvenLevelUpgrade();

    public  void UpgradeLevelPref(string skillName)
    {
        PlayerPrefs.SetInt(skillName, PlayerPrefs.GetInt(skillName, 0) + 1);
    }

    public void SetUpgradeInfos(TMP_Text levelText, TMP_Text cardType, TMP_Text upgradeInfos, Image cardImage)
    {
        levelText.text = "" + _skillLevel;
        cardImage.sprite = _cardImage;


        if ((_skillLevel + 1) % 2 == 1)
        {
            cardType.text = _skillName + " " + _oddSkillName;
            upgradeInfos.text = _skillLevel + " -> " + _skillLevel + 1;
        }
        else
        {
            cardType.text = _skillName + " " + _evenSkillName;
            upgradeInfos.text = _skillLevel + " -> " + _skillLevel + 1;
        }
    }
}
