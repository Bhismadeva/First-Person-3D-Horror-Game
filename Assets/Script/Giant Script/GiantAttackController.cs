using System.Collections;
using UnityEngine;

public class GiantAttackPattern : MonoBehaviour
{
    private Animator animator;
    public int playerArtifactsCollected = 0;
    private float timeBetweenAttacks;
    private float attackTimer;

    void Start()
    {
        animator = GetComponent<Animator>();
        SetStageParameters();
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= timeBetweenAttacks)
        {
            PerformRandomAttack();
            attackTimer = 0;
        }

        // Update stage based on collected artifacts
        if (playerArtifactsCollected >= 0 && playerArtifactsCollected < 1)
        {
            SetStageParameters(1);
        }
        else if (playerArtifactsCollected >= 1 && playerArtifactsCollected < 2)
        {
            SetStageParameters(2);
        }
        else if (playerArtifactsCollected >= 2)
        {
            SetStageParameters(3);
        }
    }

    void SetStageParameters()
    {
        SetStageParameters(playerArtifactsCollected >= 2 ? 3 : playerArtifactsCollected >= 1 ? 2 : 1);
    }

    void SetStageParameters(int stage)
    {
        switch (stage)
        {
            case 1:
                timeBetweenAttacks = 2.0f;
                animator.speed = 0.2f;
                break;
            case 2:
                timeBetweenAttacks = 1.5f;
                animator.speed = 0.5f;
                break;
            case 3:
                timeBetweenAttacks = 1.0f;
                animator.speed = 1.0f;
                break;
        }
    }

    void PerformRandomAttack()
    {
        int attackType = Random.Range(0, 3); // 0 for Swing, 1 for Hammer, 2 for Swing-Hammer-Swing
        animator.SetInteger("AttackType", attackType);

        switch (attackType)
        {
            case 0:
                animator.SetTrigger("Swing");
                break;
            case 1:
                animator.SetTrigger("Hammer");
                break;
            case 2:
                StartCoroutine(ComboAttack());
                break;
        }
        StartCoroutine(ReturnToIdle());
    }

    IEnumerator ComboAttack()
    {
        animator.SetTrigger("Swing");
        yield return new WaitForSeconds(0.5f); // Adjust time according to animation length
        animator.SetTrigger("Hammer");
        yield return new WaitForSeconds(0.5f); // Adjust time according to animation length
        animator.SetTrigger("Swing");
    }

    IEnumerator ReturnToIdle()
    {
        yield return new WaitForSeconds(1.0f); // Adjust time to match the longest attack animation
        animator.SetTrigger("Idle");
    }
}
