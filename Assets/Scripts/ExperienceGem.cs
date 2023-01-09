using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExperienceGem : MonoBehaviour
{
    private Collider col;
    private PlayerManager playerManager;

    private void Start()
    {
        col = GetComponent<Collider>();
        playerManager = PlayerManager.Instance;
    }

    public void JumpToPlayer()
    {
        col.enabled = false;
        Vector3 targetPos = playerManager.GetCharacterTransform().position;
        transform.DOJump(targetPos, 1.5f, 1, 0.1f).OnComplete(() =>
        {
            col.enabled = true;
            gameObject.SetActive(false);
            playerManager.setXp = 5;
        });
    }
}
