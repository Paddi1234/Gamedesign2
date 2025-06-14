using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private int requiredItems = 3;
    [SerializeField] private DoorController doorToOpen;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private float messageDuration = 2f;

    [Header("Sprites")]
    [SerializeField] private Sprite inactiveSprite; // rot
    [SerializeField] private Sprite activeSprite;   // grün

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
            ItemCounter counter = Object.FindFirstObjectByType<ItemCounter>();
            if (counter == null)
            {
                Debug.LogWarning("ItemCounter nicht gefunden!");
                return;
            }

            Debug.Log($"Checkpoint betreten. Items: {counter.GetItemCount()} / {requiredItems}");

            counter.SetTarget(requiredItems); // wichtig!

            if (counter.GetItemCount() >= requiredItems)
            {
                isActivated = true;
                counter.DeliverItems(requiredItems);

                GameManager.Instance.SetRespawnPoint(transform.position);
                Debug.Log("Checkpoint aktiviert!");

                if (doorToOpen != null)
                    doorToOpen.OpenDoor();

                if (spriteRenderer != null && activeSprite != null)
                {
                    spriteRenderer.sprite = activeSprite;
                    Debug.Log("Sprite auf GRÜN gesetzt!");
                }
            }
            else
            {
                int fehlende = requiredItems - counter.GetItemCount();
                ShowFeedback($"Du brauchst noch {fehlende} Items, um weiterzukommen!");
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
