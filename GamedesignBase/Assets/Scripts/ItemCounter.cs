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



