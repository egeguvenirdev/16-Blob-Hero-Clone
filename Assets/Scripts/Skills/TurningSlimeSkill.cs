using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class TurningSlimeSkill : SkillBase
{
    [Header("Turning Slime Skill Uthilities")]
    [SerializeField] private GameObject _turningSlimeParent;
    [SerializeField] private float _slimeSpeed;
    [SerializeField] private float damage = 5f;

    private ObjectPooler objectPooler;

    private List<GameObject> slimes = new List<GameObject>();

    public override void Initialize()
    {
        objectPooler = ObjectPooler.Instance;
        if (PlayerPrefs.GetInt(_skillName, 0) >= 1)
        {        
            OpenSlimes();
        }
    }

    public override void DeInitialize()
    {
        PlayerPrefs.SetInt(_skillName, 0);
        PlayerPrefs.SetFloat(_oddSkillName, 1);
        PlayerPrefs.SetFloat(_evenSkillName, 1);
    }

    private void Update()
    {
        if (_turningSlimeParent.activeSelf == true)
        {
            _turningSlimeParent.transform.Rotate(Vector3.up * -_slimeSpeed * PlayerPrefs.GetFloat(_evenSkillName, 1f) * Time.deltaTime * 20);
        }       
    }

    protected override void OddLevelUpgrade()
    {
        PlayerPrefs.SetFloat(_oddSkillName, PlayerPrefs.GetFloat(_oddSkillName, 1) + _skillOddValue);
        OpenSlimes();
    }

    protected override void EvenLevelUpgrade()
    {
        PlayerPrefs.SetFloat(_evenSkillName, PlayerPrefs.GetFloat(_evenSkillName, 0) + _skillEvenValue);
        OpenSlimes();
    }

    private void OpenSlimes()
    {
        _turningSlimeParent.SetActive(true);
        ResetSlimesParent();

        float slimeCount = PlayerPrefs.GetFloat(_oddSkillName, 2);
        float slimeRotateY = 360 / slimeCount ;
        for (int i = 0; i < slimeCount; i++)
        {
            // Distance around the circle
            var radians = 2 * Mathf.PI / slimeCount * i;

            // direction
            var vertical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);

            var spawnDir = new Vector3(horizontal, 0, vertical);
            var spawnPos = Vector3.zero + spawnDir * 1.5f; // Radius is just the distance away from the point

            var enemy = objectPooler.GetPooledObject("Slime");
            slimes.Add(enemy);
            enemy.transform.parent = _turningSlimeParent.transform;
            enemy.transform.localPosition = spawnPos + Vector3.up;
            enemy.SetActive(true);
            //enemy.transform.LookAt(point);
        }
    }

    private void ResetSlimesParent()
    {
        if(slimes.Count > 0)
        {
            for (int i = 0; i < slimes.Count; i++)
            {
                slimes[i].transform.parent = objectPooler.transform;
                slimes[i].SetActive(false);
            }
        }
        slimes.Clear();
    }
}
