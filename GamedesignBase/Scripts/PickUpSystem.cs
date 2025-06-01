using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    // Optional: Sound oder Effekt sp�ter einf�gen
    //[SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Pr�fen ob der Player das Item ber�hrt
        if (other.CompareTag("Player"))
        {
            // Optional: Sound abspielen
           // if (pickupSound != null)
            //{
             //   AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            //}

            // Objekt "aufsammeln" = zerst�ren
            Destroy(gameObject);
        }
    }
}
