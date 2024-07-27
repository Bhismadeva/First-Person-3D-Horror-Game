using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float waitTimeAtLastSeen = 5f; // waktu menunggu di last seen
    public Transform player;

    private NavMeshAgent agent;
    private int currentPatrolIndex;
    private bool playerDetected;
    private bool isChasing;
    private bool waitingAtLastSeen;
    private Vector3 lastSeenPlayerPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentPatrolIndex = 0;
        playerDetected = false;
        isChasing = false;
        waitingAtLastSeen = false;

        GoToNextPatrolPoint();
    }

    void Update()
    {
        if (playerDetected)
        {
            ChasePlayer();
            isChasing = true;
            waitingAtLastSeen = false;
        }
        else if (isChasing)
        {
            if (agent.pathStatus == NavMeshPathStatus.PathPartial)
            {
                StartCoroutine(WaitAtNavMeshBorder());
            }
            else
            {
                GoToLastSeenPosition();
            }
        }
        else if (waitingAtLastSeen)
        {
            // wait logic
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }
    }

    private void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        agent.destination = patrolPoints[currentPatrolIndex].position;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    private void ChasePlayer()
    {
        agent.destination = player.position;
    }

    private void GoToLastSeenPosition()
    {
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(lastSeenPlayerPosition, path);
        if (path.status == NavMeshPathStatus.PathComplete)
        {
            agent.destination = lastSeenPlayerPosition;
            if (Vector3.Distance(transform.position, lastSeenPlayerPosition) < 0.5f)
            {
                StartCoroutine(WaitAtLastSeenPosition());
            }
        }
        else
        {
            // If path is not complete, wait at the border
            StartCoroutine(WaitAtNavMeshBorder());
        }
    }

    private IEnumerator WaitAtLastSeenPosition()
    {
        waitingAtLastSeen = true;
        yield return new WaitForSeconds(waitTimeAtLastSeen);
        waitingAtLastSeen = false;
        isChasing = false;
        GoToNextPatrolPoint();
    }

    private IEnumerator WaitAtNavMeshBorder()
    {
        waitingAtLastSeen = true;
        yield return new WaitForSeconds(waitTimeAtLastSeen);
        waitingAtLastSeen = false;
        isChasing = false;
        GoToNextPatrolPoint();
    }

    public void SetPlayerDetected(bool detected)
    {
        playerDetected = detected;
        if (detected)
        {
            lastSeenPlayerPosition = player.position;
        }
    }
}
