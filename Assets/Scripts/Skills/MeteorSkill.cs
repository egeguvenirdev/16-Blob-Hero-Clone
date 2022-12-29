using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSkill : SkillBase
{
    [SerializeField] private int diameter = 10;
    [SerializeField] private int height = 10;

    public override void Initialize()
    {

    }

    protected override void OddLevelUpgrade()
    {

    }

    protected override void EvenLevelUpgrade()
    {

    }

    private Vector3 GetRandomPoint(Vector3 circlePos, float radius, float scale)
    {
        Vector3 randomPoint = Vector3.up * 10 + (Random.insideUnitSphere * diameter);
        randomPoint.y = height;
        return randomPoint;
    }
}
