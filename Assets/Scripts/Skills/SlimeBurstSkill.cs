using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBurstSkill : SkillBase
{
    [Header("Burst Slime Skill Uthilities")]
    [SerializeField] private GameObject burstSlime;
    [SerializeField] private float speed = 5;
    [SerializeField] private bool isActive = true;

    public override void Initialize()
    {
        if (PlayerPrefs.GetInt(_skillName, 0) >= 1)
        {
            StartCoroutine(ThrowSlimes());
        }
    }

    protected override void OddLevelUpgrade()
    {
        PlayerPrefs.SetFloat(_oddSkillName, PlayerPrefs.GetFloat(_oddSkillName, 0) + _skillOddValue);
        StartCoroutine(ThrowSlimes());
    }

    protected override void EvenLevelUpgrade()
    {
        PlayerPrefs.SetFloat(_evenSkillName, PlayerPrefs.GetFloat(_evenSkillName, 0) + _skillEvenValue);
    }

    private IEnumerator ThrowSlimes()
    {
        while (isActive)
        {
            float rotateAngle = 360 / PlayerPrefs.GetFloat(_oddSkillName, 1);

            for (int i = 0; i < PlayerPrefs.GetFloat(_oddSkillName, 1); i++)
            {
                GameObject instantiatedMeteor = ObjectPooler.Instance.GetPooledObject("BurstSlime");
                instantiatedMeteor.transform.position = Vector3.up;
                instantiatedMeteor.transform.rotation = Quaternion.Euler(0, rotateAngle, 0);
                //instantiatedMeteor.transform.SetParent(ground.transform);
                instantiatedMeteor.SetActive(true);
                instantiatedMeteor.GetComponent<InstantiatedBurstSlime>().ReleaseTheSlimes();
            }
            yield return new WaitForSeconds(5f / PlayerPrefs.GetFloat(_evenSkillName, 1));
        }
    }
}
