using UnityEngine;
using TMPro;

public class ItemCounter : MonoBehaviour
{
    [Header("Item Einstellungen")]
    public int totalItems = 4;
    private int collectedItems = 0;

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

    public bool CanDeliver()
    {
        return collectedItems >= totalItems;
    }

    public void DeliverItems()
    {
        if (CanDeliver())
        {
            Debug.Log("Alle Items abgegeben!");
            collectedItems = 0;
            UpdateUI();

           if (door != null)
            {
                door.OpenDoor();
            }
        }
        else
        {
            Debug.Log("Noch nicht genug gesammelt.");
        }
    }

    private void UpdateUI()
    {
        counterText.text = $"Gesammelt: {collectedItems} / {totalItems}";
    }
}