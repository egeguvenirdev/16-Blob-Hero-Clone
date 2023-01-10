using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class TurningSlimeSkill : SkillBase
{
    [Header("Turning Slime Skill Uthilities")]
    [SerializeField] private GameObject _turningSlimeParent;
    [SerializeField] private GameObject _turningSlime1;
    [SerializeField] private GameObject _turningSlime2;
    [SerializeField] private float _slimeHeight;
    [SerializeField] private float _slimeSpeed;
    [SerializeField] private float _rangeUpgradeValue;
    [SerializeField] private float damage = 5f;

    public override void Initialize()
    {
        if (PlayerPrefs.GetInt(_skillName, 0) >= 1)
        {
            OpenSlimes();
        }
    }

    private void Update()
    {
        if (_turningSlimeParent.activeSelf == true)
        {
            _turningSlimeParent.transform.Rotate(Vector3.up *_slimeSpeed * PlayerPrefs.GetFloat(_oddSkillName, 0.5f) * Time.deltaTime * 10);
        }       
    }

    protected override void OddLevelUpgrade()
    {
        PlayerPrefs.SetFloat(_oddSkillName, PlayerPrefs.GetFloat(_oddSkillName, 0) + _skillOddValue);
        OpenSlimes();
    }

    protected override void EvenLevelUpgrade()
    {
        PlayerPrefs.SetFloat(_evenSkillName, PlayerPrefs.GetFloat(_evenSkillName, 0) + _rangeUpgradeValue);
        OpenSlimes();
    }

    private void OpenSlimes()
    {
        _turningSlimeParent.SetActive(true);

        _turningSlime1.transform.localPosition = new Vector3(0, _slimeHeight, PlayerPrefs.GetFloat(_evenSkillName, 0) + _skillEvenValue);
        _turningSlime2.transform.localPosition = new Vector3(0, _slimeHeight, -(PlayerPrefs.GetFloat(_evenSkillName, 0) + _skillEvenValue));
        //StartRotate();
    }
}
