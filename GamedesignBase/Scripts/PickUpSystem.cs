using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    // Optional: Sound oder Effekt später einfügen
    //[SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Prüfen ob der Player das Item berührt
        if (other.CompareTag("Player"))
        {
            // Optional: Sound abspielen
           // if (pickupSound != null)
            //{
             //   AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            //}

            // Objekt "aufsammeln" = zerstören
            Destroy(gameObject);
        }
    }
}
