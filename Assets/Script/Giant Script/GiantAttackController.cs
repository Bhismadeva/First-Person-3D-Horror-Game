//GOOD
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

    void Start()
    {
        animator = GetComponent<Animator>();
        SetStageParameters();
        animator.SetTrigger("Idle"); // Start with idle animation
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= timeBetweenAttacks)
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
                animator.SetTrigger("Idle");
            }
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

    void HandleTrigger1()
    {
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

    void HandleTrigger2()
    {
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

    void HandleTrigger3()
    {
        PerformSwingAttack();
    }

    void PerformHammerAttack()
    {
        animator.SetTrigger("Hammer");
    }

    void PerformSwingAttack()
    {
        animator.SetTrigger("Swing");
    }

    void FlipGiant(float scaleX)
    {
        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
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

