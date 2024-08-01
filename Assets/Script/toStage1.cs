using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public bool isPuzzleCompleted = false; // Set this from your puzzle script
    private bool isNearDoor = false;
    public GameObject currentArea; // The area to unload
    public GameObject nextArea; // The area to load
    public Transform spawnPoint; // The target position to teleport the player
    public GameObject door; // Assign the door GameObject in the Inspector

    void Update()
    {
        if (isNearDoor && isPuzzleCompleted && Input.GetKeyDown(KeyCode.F))
        {
            MovePlayerToNextArea();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearDoor = true;
            // Show "Press F to interact" UI
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearDoor = false;
            // Hide "Press F to interact" UI
        }
    }

    private void MovePlayerToNextArea()
    {
        // Unload current area
        currentArea.SetActive(false);

        // Load next area
        nextArea.SetActive(true);

        // Move player to the other side of the door
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Debug.Log("Teleporting player to: " + spawnPoint.position);
            Debug.Log("Spawn point position from script: " + spawnPoint.position);
            Debug.Log("Spawn point local position: " + spawnPoint.localPosition);
            Debug.Log("Spawn point parent: " + (spawnPoint.parent != null ? spawnPoint.parent.name : "No parent"));

            // Handle CharacterController if present
            CharacterController controller = player.GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.enabled = false;  // Disable before teleporting
            }

            // Handle Rigidbody if present
            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;  // Disable physics before teleporting
            }

            // Teleport player
            player.transform.position = spawnPoint.position;

            Debug.Log("Player's new position: " + player.transform.position);

            // Re-enable Rigidbody if needed
            if (rb != null)
            {
                rb.isKinematic = false;  // Re-enable physics after teleporting
                rb.velocity = Vector3.zero;  // Reset velocity
            }

            // Re-enable CharacterController if needed
            if (controller != null)
            {
                controller.enabled = true;  // Re-enable after teleporting
            }
        }
        else
        {
            Debug.LogWarning("Player not found!");
        }

        // Optionally, disable the door or handle its state
        door.SetActive(false);
    }
}
