using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Holt direkt die Position aus dem GameManager
            if (GameManager.Instance != null)
            {
                Vector3 respawnPoint = GameManager.Instance.GetRespawnPoint();
                other.transform.position = respawnPoint;
                Debug.Log("Spieler wurde nach Tod durch DeathZone zum Checkpoint zurückgesetzt.");
            }
            else
            {
                Debug.LogWarning("GameManager nicht gefunden – Respawn nicht möglich.");
            }
        }
    }
}


