using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MagnetSkill : SkillBase
{
    [Header("Magnet Skill Uthilities")]
    [SerializeField] private ParticleSystem collectingParticle;
    [SerializeField] private bool isActive = true;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Transform objectPooler;

    public override void Initialize()
    {
        if (PlayerPrefs.GetInt(_skillName, 0) >= 1)
        {
            StartCoroutine(PullTheDiamonds());
        }
    }

    protected override void OddLevelUpgrade()
    {
        PlayerPrefs.SetFloat(_oddSkillName, PlayerPrefs.GetFloat(_oddSkillName, 0) + _skillOddValue);
        StartCoroutine(PullTheDiamonds());
    }

    protected override void EvenLevelUpgrade()
    {
        PlayerPrefs.SetFloat(_evenSkillName, PlayerPrefs.GetFloat(_evenSkillName, 0) + _skillEvenValue);

        float particleSpeed = 0.2f * PlayerPrefs.GetFloat(_evenSkillName, 1); // set particle emission speed
        var collectingEmission = collectingParticle.emission;
        collectingEmission.rateOverTime = particleSpeed;
    }

    private IEnumerator PullTheDiamonds()
    {
        collectingParticle.gameObject.SetActive(true);
        float particleScale = 1 + 0.2f * PlayerPrefs.GetFloat(_evenSkillName, 1);
        collectingParticle.transform.localScale = new Vector3(particleScale, particleScale, particleScale);
        while (isActive)
        {
            MagnetCalculation();
            yield return new WaitForSeconds(5 / PlayerPrefs.GetFloat(_evenSkillName, 1));
        }
    }

    private void MagnetCalculation()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, _skillOddValue * 2, layer);

        foreach (Collider gems in collider)
        {
            gems.transform.DOJump(Vector3.zero, 3, 1, 0.4f).OnComplete( () => 
            {
                gems.transform.SetParent(objectPooler);
                gems.gameObject.SetActive(false);
            });
        }
    }
}
