using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ItemCounter counter = Object.FindFirstObjectByType<ItemCounter>();
            if (counter != null)
            {
                counter.DeliverItems();
            }
        }
    }
}
