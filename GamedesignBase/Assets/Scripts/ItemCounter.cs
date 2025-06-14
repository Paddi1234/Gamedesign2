using UnityEngine;
using TMPro;

public class ItemCounter : MonoBehaviour
{
    private int collectedItems = 0;
    private int targetItems = 0;

    [Header("UI")]
    public TextMeshProUGUI counterText;

    [Header("Tür (optional)")]
    public DoorController door;

    private void Start()
    {
        UpdateUI();
    }

    public void AddItem()
    {
        collectedItems++;
        UpdateUI();
    }

    public int GetItemCount()
    {
        return collectedItems;
    }

    public void DeliverItems(int amount)
    {
        if (collectedItems >= amount)
        {
            collectedItems -= amount;
            Debug.Log($"Abgegeben: {amount} Items.");
            UpdateUI();

            if (door != null && collectedItems >= targetItems)
            {
                door.OpenDoor();
            }
        }
        else
        {
            Debug.Log($"Nicht genug Items zum Abgeben: {collectedItems} / {amount}");
        }
    }

    public void SetTarget(int amount)
    {
        targetItems = amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        counterText.text = $"Gesammelt: {collectedItems} / {targetItems}";
    }
}



