using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    public GameObject gameOverCanvas;

    void Start()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false); // Nonaktifkan canvas di awal
        }
        else
        {
            Debug.LogError("Game Over Canvas tidak diatur di inspector.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (gameOverCanvas != null)
            {
                gameOverCanvas.SetActive(true);
                Debug.Log("Game Over Canvas activated.");
                Time.timeScale = 0f; // Stop the game
            }
        }
    }
}
