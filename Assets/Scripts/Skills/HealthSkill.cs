using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSkill : SkillBase
{
    public override void Initialize()
    {
        UpgradePlayer(PlayerPrefs.GetFloat(_oddSkillName, 0), PlayerPrefs.GetFloat(_evenSkillName, 0), 0);
    }

    public override void DeInitialize()
    {
        PlayerPrefs.SetInt(_skillName, 0);
        PlayerPrefs.SetFloat(_oddSkillName, 0);
        PlayerPrefs.SetFloat(_evenSkillName, 0);
    }


    protected override void OddLevelUpgrade()
    {
        PlayerPrefs.SetFloat(_oddSkillName, PlayerPrefs.GetFloat(_oddSkillName, 0) + _skillOddValue);
        UpgradePlayer(PlayerPrefs.GetFloat(_oddSkillName, 0), PlayerPrefs.GetFloat(_evenSkillName, 0), _skillOddValue);
    }

    protected override void EvenLevelUpgrade()
    {
        PlayerPrefs.SetFloat(_evenSkillName, PlayerPrefs.GetFloat(_evenSkillName, 0) + _skillEvenValue);
        UpgradePlayer(PlayerPrefs.GetFloat(_oddSkillName, 0), PlayerPrefs.GetFloat(_evenSkillName, 0), 0);
    }

    private void UpgradePlayer(float maxHealthUpgrade, float healthRegen, float boostCurrentHealth)
    {
        //Debug.Log("Health Skill Values ->:  Max Health" + PlayerPrefs.GetFloat(_oddSkillName, 0) + " Helath Regen: " + PlayerPrefs.GetFloat(_oddSkillName, 0));
        PlayerManager.Instance.SetHealthAndStats(maxHealthUpgrade, healthRegen, boostCurrentHealth);
    }
}
