using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    [Header("UI")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Respawn")]
    public Transform respawnPoint;

    [Header("Audio")]
    public AudioClip hurtSound;
    private AudioSource audioSource;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHearts();
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        if (hurtSound != null)
            audioSource.PlayOneShot(hurtSound);

        UpdateHearts();

        if (currentHealth <= 0)
        {
            Respawn();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Prüfe, ob es KEIN Treffer von oben ist
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y >= 0.5f)
                {
                    // Treffer von oben → kein Schaden
                    return;
                }
            }

            // Kein Treffer von oben → Schaden nehmen
            TakeDamage(1);
        }
    }
    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    private void Respawn()
    {
        currentHealth = maxHealth;
        UpdateHearts();
        transform.position = respawnPoint.position;
    }
}