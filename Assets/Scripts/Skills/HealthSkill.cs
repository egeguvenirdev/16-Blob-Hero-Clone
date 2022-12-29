using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSkill : SkillBase
{
    public override void Initialize()
    {
        UpgradePlayer();
    }

    protected override void OddLevelUpgrade()
    {
        PlayerPrefs.SetFloat(_oddSkillName, PlayerPrefs.GetFloat(_oddSkillName, 0) + _skillOddValue);
        UpgradePlayer();
    }

    protected override void EvenLevelUpgrade()
    {
        PlayerPrefs.SetFloat(_evenSkillName, PlayerPrefs.GetFloat(_evenSkillName, 0) + _skillEvenValue);
        UpgradePlayer();
    }

    private void UpgradePlayer()
    {
        PlayerManager.Instance.SetHealthStats(PlayerPrefs.GetFloat(_oddSkillName, 0), PlayerPrefs.GetFloat(_evenSkillName, 0));
    }
}
