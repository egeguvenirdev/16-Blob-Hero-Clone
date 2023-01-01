using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSkill : SkillBase
{
    [SerializeField] private ParticleSystem collectingParticle;
    [SerializeField] private bool isActive = true;
    [SerializeField] private LayerMask layer;

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
    }

    protected override void EvenLevelUpgrade()
    {
        PlayerPrefs.SetFloat(_evenSkillName, PlayerPrefs.GetFloat(_evenSkillName, 0) + _skillEvenValue);
    }

    private IEnumerator PullTheDiamonds()
    {
        while (isActive)
        {
            GameObject instantiatedMeteor = ObjectPooler.Instance.GetPooledObject("CollectParticle");
            instantiatedMeteor.transform.position = transform.position;
            instantiatedMeteor.transform.rotation = Quaternion.Euler(0, 0, 0);
            MagnetCalculation();
            instantiatedMeteor.SetActive(true);
            yield return new WaitForSeconds(5f / PlayerPrefs.GetFloat(_evenSkillName, 1));
        }
    }

    private void MagnetCalculation()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, _skillOddValue * 2, layer);

        foreach (Collider boxes in collider)
        {
            Rigidbody rb = boxes.GetComponentInChildren<Rigidbody>(); // collect the diamonds
        }
    }
}
