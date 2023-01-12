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
    public static event Action skillSelected;

    public TMP_Text currentLevel;
    public TMP_Text skillName;
    public TMP_Text skillStat;
    public Button button;
    public Image cardImage;

    [NonSerialized] public SkillBase SkillBase;
    private bool isActive = false;

    private void Awake()
    {
        CardData.skillSelected += OnSkillSelected;
    }

    public void SetCardInfo(SkillBase skillBase)
    {
        isActive = true;
        currentLevel.text = "" + PlayerPrefs.GetInt(skillBase.LevelKey, 0) + "LEVEL";
        cardImage.sprite = skillBase.Image;
        SkillBase = skillBase;

        int remainer = (PlayerPrefs.GetInt(skillBase.LevelKey, 0) + 1) % 2;
        bool isLevelOdd = remainer % 2 == 1;

        string skillname = isLevelOdd ? skillBase.SkillNames[0] : skillBase.SkillNames[1];
        float skillValue = isLevelOdd ? skillBase.SkillValues[0] : skillBase.SkillValues[1];

        skillName.text = skillname;
        skillStat.text = SkillStatCalculator(skillname, skillValue);
    }

    private string SkillStatCalculator(string s, float f) 
    {
        return PlayerPrefs.GetFloat(s, f) + " -> " + (PlayerPrefs.GetFloat(s, 0) + f + f);
    }

    public void ButtonOnclick()
    {
        PlayerPrefs.SetInt("FirstUpgrade", 1); // first selection has been decided
        GameManager.Haptic(0);
        if (!isActive) return;
        skillSelected?.Invoke();
        Debug.Log("cardselect");
        transform.DOKill(true);
        transform.DOPunchScale(Vector3.one * 0.3f, 0.5f, 6).SetUpdate(true).OnComplete( () => {
            SkillBase.UpgradeButton();
            Debug.Log("closepanel");
            UIManager.Instance.CloseUpgradeCardPanel();
        });
        
    }

    private void OnSkillSelected() => isActive = false;
}
