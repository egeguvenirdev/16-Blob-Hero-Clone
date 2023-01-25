using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSkill : SkillBase
{
    //[Header("Arrow Slime Skill Uthilities")]
    public override void Initialize()
    {
    }
    public override void DeInitialize()
    {
        PlayerPrefs.SetInt(_skillName, 0);
        PlayerPrefs.SetFloat(_oddSkillName, 0);
        PlayerPrefs.SetFloat(_evenSkillName, 0);
    }

    protected override void OddLevelUpgrade()
    {

    }

    protected override void EvenLevelUpgrade()
    {

    }
}
