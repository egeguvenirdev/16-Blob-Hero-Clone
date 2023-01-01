using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSkill : SkillBase
{
    [SerializeField] private int diameter = 10;
    [SerializeField] private int height = 10;
    [SerializeField] private bool isActive = true;
    [SerializeField] private GameObject meteor;
    [SerializeField] private GameObject ground;

    public override void Initialize()
    {
        if (PlayerPrefs.GetInt(_skillName, 0) >= 1)
        {
            StartCoroutine(MakeTheMeteorRain());
        }
    }

    protected override void OddLevelUpgrade()
    {
        PlayerPrefs.SetFloat(_oddSkillName, PlayerPrefs.GetFloat(_oddSkillName, 0) + _skillOddValue);
        StartCoroutine(MakeTheMeteorRain());
    }

    protected override void EvenLevelUpgrade()
    {
        PlayerPrefs.SetFloat(_evenSkillName, PlayerPrefs.GetFloat(_evenSkillName, 0) + _skillEvenValue);
    }

    private IEnumerator MakeTheMeteorRain()
    {
        while (isActive)
        {
            for (int i = 0; i < PlayerPrefs.GetFloat(_oddSkillName, 0); i++)
            {
                GameObject instantiatedMeteor = ObjectPooler.Instance.GetPooledObject("Meteor");     
                instantiatedMeteor.transform.position = GetRandomPoint();
                instantiatedMeteor.transform.rotation = Quaternion.Euler(0, 0, -90);
                instantiatedMeteor.GetComponent<InstantiatedMeteor>().RainToEnemies();
                instantiatedMeteor.SetActive(true);             
            }
            yield return new WaitForSeconds(5f / PlayerPrefs.GetFloat(_evenSkillName, 1));
        }
    }

    private Vector3 GetRandomPoint()
    {
        Vector3 randomPoint = Vector3.up * 10 + (Random.insideUnitSphere * diameter);
        randomPoint.y = height;
        return randomPoint;
        Debug.Log(randomPoint);
    }
}
