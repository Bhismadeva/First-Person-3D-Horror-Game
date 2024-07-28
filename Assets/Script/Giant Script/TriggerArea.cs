using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public GiantAttackPattern giantAttackPattern;
    public int triggerID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            giantAttackPattern.SetPlayerInTrigger(triggerID, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            giantAttackPattern.SetPlayerInTrigger(triggerID, false);
        }
    }
}
