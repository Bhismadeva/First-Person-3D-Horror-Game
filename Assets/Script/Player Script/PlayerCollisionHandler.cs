using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public float knockbackForce = 10f; // Kekuatan knockback

    private Rigidbody playerRigidbody;

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

        playerRigidbody = GetComponent<Rigidbody>();
        if (playerRigidbody == null)
        {
            Debug.LogError("Rigidbody tidak ditemukan pada Player.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GiantHand"))
        {
            // Mendapatkan arah knockback
            Vector3 knockbackDirection = (transform.position - collision.transform.position).normalized;

            // Menambahkan gaya knockback pada player
            playerRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (gameOverCanvas != null)
            {
                gameOverCanvas.SetActive(true);
                Debug.Log("Game Over Canvas activated.");
                Time.timeScale = 0f; // Menghentikan permainan
            }
        }
    }
}
