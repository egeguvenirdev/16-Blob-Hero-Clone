using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSkill : SkillBase
{
    public override void Initialize()
    {

    }

    public override void OddLevelUpgrade()
    {
        PlayerPrefs.SetFloat("HealthIncrease", PlayerPrefs.GetFloat("HealthIncrease", 0) + _skillOddValue);
        PlayerPrefs.SetFloat("Health", PlayerPrefs.GetFloat("Health") + PlayerPrefs.GetFloat("HealthIncrease"));
        PlayerManager.Instance.UpdateHealth();
        UpgradeLevelPref(_oddSkillName);
    }

    public override void EvenLevelUpgrade()
    {
        PlayerPrefs.SetFloat("HealthRegen", PlayerPrefs.GetFloat("HealthRegen", 0) + _skillEvenValue);
        PlayerManager.Instance.UpdateHealth();
        UpgradeLevelPref(_evenSkillName);
    }
}
