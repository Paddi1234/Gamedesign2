using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float bounceForce = 10f;
    private Animator animator;
    private bool IsDead = false;

    public AudioClip deathSound;
    private AudioSource audioSource;

    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void Die()
    {
        if (IsDead) return;
        IsDead = true;

        if (animator != null)
            animator.SetBool("IsDead", true);

        if (deathSound != null && audioSource != null)
            audioSource.PlayOneShot(deathSound);

        Destroy(gameObject, 0.5f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsDead) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            // Richtung des Kontakts prüfen
            ContactPoint2D contact = collision.GetContact(0);
            Vector2 normal = contact.normal;

            bool hitFromAbove = normal.y < -0.5f;

            if (hitFromAbove)
            {
                // Spieler springt auf Gegner → Gegner stirbt
                IsDead = true;

                if (animator != null)
                    animator.SetBool("IsDead", true);

                if (deathSound != null && audioSource != null)
                    audioSource.PlayOneShot(deathSound);

                if (playerRb != null)
                    playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, bounceForce);

                // Zerstörung nach Zeit (nach Animation)
                Destroy(gameObject, 0.5f);
            }
            else
            {
                // Spieler trifft Gegner von der Seite → Schaden
                playerHealth?.TakeDamage(1);
            }
        }
    }
}





