using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GemCollecter : MonoBehaviour
{
    private PlayerManager playerManager;
    private ObjectPooler objectPooler;
    void Start()
    {
        playerManager = PlayerManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gem"))
        {
            GameManager.Haptic(0);
            other.GetComponent<ExperienceGem>().JumpToPlayer();
        }
    }
}
