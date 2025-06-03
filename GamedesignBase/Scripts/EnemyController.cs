using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;

    public AudioClip deathSound;
    private bool isDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        // Animation starten
        if (animator != null)
        {
            animator.SetTrigger("isDead");
        }

        // Sound abspielen
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        // Gegner nach kurzer Zeit zerstören (z. B. nach Animation/Sound)
        Destroy(gameObject, 0.5f);
    }
}
