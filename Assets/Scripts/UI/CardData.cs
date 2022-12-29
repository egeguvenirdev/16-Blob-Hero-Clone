using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

[Serializable]
public class CardData : MonoBehaviour
{
    public TMP_Text currentLevel;
    public TMP_Text skillName;
    public TMP_Text skillStat;
    public Button button;
    public Image cardImage;

    [NonSerialized] public SkillBase SkillBase;

    public void SetCardInfo(SkillBase skillBase)
    {
        currentLevel.text = "" + PlayerPrefs.GetInt(skillBase.LevelKey, 0) + "LEVEL";
        cardImage.sprite = skillBase.Image;
        SkillBase = skillBase;

        int remainer = (PlayerPrefs.GetInt(skillBase.LevelKey, 0) + 1) % 2;
        bool isLevelOdd = remainer % 2 == 1;

        string skillname = isLevelOdd ? skillBase.SkillNames[0] : skillBase.SkillNames[1];
        float skillValue = isLevelOdd ? skillBase.SkillValues[0] : skillBase.SkillValues[1];

        skillName.text = skillname;
        Debug.Log(skillname + " " + PlayerPrefs.GetFloat(skillname, 0));
        skillStat.text = SkillStatCalculator(skillname, skillValue);
    }

    private string SkillStatCalculator(string s, float f) 
    {
        return PlayerPrefs.GetFloat(s, 0) + " -> " + (PlayerPrefs.GetFloat(s, 0) + f);
    }

    public void ButtonOnclick()
    {
        transform.DOPunchScale(Vector3.one * 0.3f, 0.5f, 6).OnComplete( () => {
            SkillBase.UpgradeButton();
            UIManager.Instance.CloseUpgradeCardPanel();
        });
        
    }
}
