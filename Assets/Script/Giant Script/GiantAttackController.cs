using System.Collections;
using UnityEngine;

public class GiantAttackPattern : MonoBehaviour
{
    private Animator animator;
    public int playerArtifactsCollected = 0;
    private float timeBetweenAttacks;
    private float attackTimer;

    public Transform player; // Reference to the player's transform
    private bool isPlayerInTrigger1;
    private bool isPlayerInTrigger2;
    private bool isPlayerInTrigger3;

    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        SetStageParameters(playerArtifactsCollected); // Set initial parameters based on artifacts collected
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= timeBetweenAttacks && !isAttacking)
        {
            if (isPlayerInTrigger1)
            {
                HandleTrigger1();
            }
            else if (isPlayerInTrigger2)
            {
                HandleTrigger2();
            }
            else if (isPlayerInTrigger3)
            {
                HandleTrigger3();
            }
            else
            {
                SetIdleState();
            }
            attackTimer = 0;
        }

        // Update stage based on collected artifacts
        SetStageParameters(playerArtifactsCollected);
    }

    void SetStageParameters(int artifactsCollected)
    {
        switch (artifactsCollected)
        {
            case 1:
                timeBetweenAttacks = 2.0f;
                animator.speed = 0.2f; // Set global speed
                animator.SetFloat("IdleSpeed", 0.8f); // Set idle animation speed
                break;
            case 2:
                timeBetweenAttacks = 1.5f;
                animator.speed = 1.0f; // Set global speed
                animator.SetFloat("IdleSpeed", 0.9f); // Set idle animation speed
                break;
            case 3:
                timeBetweenAttacks = 1.0f;
                animator.speed = 0.4f; // Set global speed
                animator.SetFloat("IdleSpeed", 1f); // Set idle animation speed
                break;
            default:
                timeBetweenAttacks = 2.0f;
                animator.speed = 0.6f; // Set global speed
                animator.SetFloat("IdleSpeed", 0.8f); // Set idle animation speed
                break;
        }
    }

    void HandleTrigger1()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.SetBool("IsIdle", false);
            if (transform.localScale.x > 0)
            {
                PerformHammerAttack();
            }
            else
            {
                FlipGiant(2.5f);
                PerformHammerAttack();
            }
        }
    }

    void HandleTrigger2()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.SetBool("IsIdle", false);
            if (transform.localScale.x < 0)
            {
                PerformHammerAttack();
            }
            else
            {
                FlipGiant(-2.5f);
                PerformHammerAttack();
            }
        }
    }

    void HandleTrigger3()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.SetBool("IsIdle", false);
            PerformSwingAttack();
        }
    }

    void PerformHammerAttack()
    {
        isAttacking = true;
        animator.SetTrigger("Hammer");
    }

    void PerformSwingAttack()
    {
        isAttacking = true;
        animator.SetTrigger("Swing");
    }

    void SetIdleState()
    {
        if (!isAttacking)
        {
            animator.SetBool("IsIdle", true);
        }
    }

    void FlipGiant(float scaleX)
    {
        // Only flip if not currently performing an attack
        if (!isAttacking)
        {
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
        }
    }

    // This method should be called when an attack animation ends
    public void OnAttackAnimationEnd()
    {
        isAttacking = false;
        SetIdleState();
    }

    public void SetPlayerInTrigger(int triggerID, bool isInTrigger)
    {
        switch (triggerID)
        {
            case 1:
                isPlayerInTrigger1 = isInTrigger;
                break;
            case 2:
                isPlayerInTrigger2 = isInTrigger;
                break;
            case 3:
                isPlayerInTrigger3 = isInTrigger;
                break;
        }
    }
}
