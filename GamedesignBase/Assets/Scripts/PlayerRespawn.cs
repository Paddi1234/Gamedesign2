using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Sidekick")]
    [SerializeField] private Transform sidekick; // Ziehe dein Sidekick-Objekt hier rein

    public void Respawn()
    {
        if (GameManager.Instance != null)
        {
            Vector3 respawnPoint = GameManager.Instance.GetRespawnPoint();

            // Spieler zur�cksetzen
            transform.position = respawnPoint;

            // Sidekick mitsetzen (leicht versetzt)
            if (sidekick != null)
            {
                sidekick.position = respawnPoint + new Vector3(-1.5f, 0f, 0f);
            }

            Debug.Log("Respawn erfolgreich � Spieler & Sidekick zur�ckgesetzt.");
        }
        else
        {
            Debug.LogWarning("Kein GameManager vorhanden � Respawn fehlgeschlagen.");
        }
    }
}

