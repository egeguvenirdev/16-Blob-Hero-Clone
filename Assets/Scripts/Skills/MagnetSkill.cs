using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSkill : SkillBase
{
    public override void Initialize()
    {
        if (PlayerPrefs.GetInt("Health", 0) == 0)
        {
            //PlayerPrefs.SetInt("Health", _health);
        }
    }

    public override void OddLevelUpgrade()
    {

    }

    public override void EvenLevelUpgrade()
    {

    }
}
