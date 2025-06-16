using UnityEngine;
using TMPro;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private int requiredItems = 3;
    [SerializeField] private DoorController doorToOpen;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private float messageDuration = 2f;

    [Header("Sprites")]
    [SerializeField] private Sprite inactiveSprite;
    [SerializeField] private Sprite activeSprite;

    [Header("System-Referenzen")]
    [SerializeField] private ItemCounter itemCounter; // im Inspector zuweisen

    private bool isActivated = false;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && inactiveSprite != null)
        {
            spriteRenderer.sprite = inactiveSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActivated && other.CompareTag("Player"))
        {
            if (itemCounter == null)
            {
                Debug.LogWarning("[Checkpoint] Kein ItemCounter zugewiesen!");
                return;
            }

            itemCounter.SetTarget(requiredItems);
            Debug.Log($"[Checkpoint] Betreten – Ziel: {requiredItems}, Gesammelt: {itemCounter.GetItemCount()}");

            if (itemCounter.GetItemCount() >= requiredItems)
            {
                isActivated = true;
                GameManager.Instance.SetRespawnPoint(transform.position);
                Debug.Log($"[Checkpoint] Aktiviert! Respawn gesetzt.");

                if (doorToOpen != null)
                    doorToOpen.OpenDoor();

                if (spriteRenderer != null && activeSprite != null)
                    spriteRenderer.sprite = activeSprite;
            }
            else
            {
                int fehlend = requiredItems - itemCounter.GetItemCount();
                ShowFeedback($"Du brauchst noch {fehlend} Items, um weiterzukommen.");
            }
        }
    }

    private void ShowFeedback(string message)
    {
        if (feedbackText == null) return;

        feedbackText.text = message;
        feedbackText.gameObject.SetActive(true);
        CancelInvoke(nameof(HideFeedback));
        Invoke(nameof(HideFeedback), messageDuration);
    }

    private void HideFeedback()
    {
        if (feedbackText != null)
            feedbackText.gameObject.SetActive(false);
    }
}