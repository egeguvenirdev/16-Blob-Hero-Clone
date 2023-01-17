using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : EnemyBase
{
    private AIManager aiManager;
    [SerializeField] private bool canMove = true;
    private Vector3 destination;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    void Start()
    {
        aiManager = FindObjectOfType<AIManager>();
        maxHealth = 100;
    }

    protected override void MoveTowardsPlayer(Vector3 player)
    {
        if (agent == null) return;

        if (player != null) // check the player if its dead or what
        {
            destination = player;
            agent.SetDestination(destination);
            //Debug.Log(agent.remainingDistance);
        }

        if (agent.remainingDistance > 0 && agent.remainingDistance < 2f)
        {
            if (canMove)
            {
                isRunning = false;
                _animancer.PlayAnimation("BossAttack");
                StartCoroutine(HitRoutine());
            }
        }
        else
        {
            if (canMove && !isRunning)
            {
                _animancer.PlayAnimation("BossRun");
                StopAllCoroutines();
                isRunning = true;
            }
        }
    }

    private IEnumerator HitRoutine()
    {
        canMove = false;
        yield return new WaitForSeconds(0.3f);
        GameManager.Haptic(1);
        playerManager.setHealth = damage;
        yield return new WaitForSeconds(0.8f);
        _animancer.Stop();
        canMove = true;
    }

    protected override void Die()
    {
        PlayParticle();
        OnBossDied();
        base.Die();
    }

    private void OnBossDied()
    {
        aiManager.BossDied();
        gameObject.SetActive(false);
    }

    private void PlayParticle()
    {
        var particle = ObjectPooler.Instance.GetPooledObject("HitParticle");
        Vector3 particlePos = new Vector3(transform.position.x, 0.25f, transform.position.z);
        particle.transform.position = particlePos;
        particle.transform.rotation = Quaternion.identity;
        particle.SetActive(true);
        particle.GetComponent<ParticleSystem>().Play();
    }
}
