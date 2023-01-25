using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSkill : SkillBase
{
    [Header("Meteor Slime Skill Uthilities")]
    [SerializeField] private int diameter = 10;
    [SerializeField] private int height = 10;
    [SerializeField] private bool isActive = true;
    [SerializeField] private GameObject meteor;
    [SerializeField] private GameObject ground;
    private PlayerManager playerManager;

    public override void Initialize()
    {
        playerManager = PlayerManager.Instance;
        if (PlayerPrefs.GetInt(_skillName, 0) >= 1)
        {
            StartCoroutine(MakeTheMeteorRain());
        }    
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
                //instantiatedMeteor.transform.SetParent(ground.transform);
                instantiatedMeteor.SetActive(true);             
            }
            yield return new WaitForSeconds(5f / PlayerPrefs.GetFloat(_evenSkillName, 1));
        }
    }

    private Vector3 GetRandomPoint()
    {
        Vector3 randomPoint = Vector3.up * 5 + (Random.insideUnitSphere * diameter);
        Vector3 playerPos = playerManager.GetCharacterTransform().position;
        randomPoint += playerPos;
        randomPoint.y = height;
        return randomPoint;
    }
}
