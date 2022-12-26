using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class SkillBase : MonoBehaviour
{
    [Header("Card Infos")]
    [SerializeField] protected Image _cardImage;
    [SerializeField] protected string _cardName;

    public void Initialize()
    {
        //
    }

    void Start()
    {
        if(PlayerPrefs.GetInt(_cardName) == 0)
        {
            PlayerPrefs.SetInt(_cardName, 1);
        }
    }

    public abstract void OddLevelUpgrade();

    public abstract void EvenLevelUpgrade();

    public void SetUpgradeInfos(TMP_Text levelText, TMP_Text cardType, TMP_Text upgradeInfos, Image cardImage)
    {

    }
}
