using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    // Optional: Sound oder Effekt später einfügen
    public AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Prüfen ob der Player das Item berührt
        if (other.CompareTag("Player"))
        {
            {
                ItemCounter counter = Object.FindFirstObjectByType<ItemCounter>();
                if (counter != null)
                {
                    counter.AddItem();
                }

                // Optional: Sound abspielen
                if (pickupSound != null)
                {
                  AudioSource.PlayClipAtPoint(pickupSound, transform.position);
                }

                // Objekt "aufsammeln" = zerstören
                Destroy(gameObject);
            }
        }
    }
}


