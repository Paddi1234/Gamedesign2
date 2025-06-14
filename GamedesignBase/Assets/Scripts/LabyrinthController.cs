using System.Collections;
using UnityEngine;

public class LabyrinthController : MonoBehaviour
{
    [Header("Bewegung")]
    public float moveSpeed = 5f;

    [Header("Respawn")]
    public Transform spawnPoint;

    [Header("Audio")]
    public AudioClip deathSound;
    private AudioSource audioSource;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        // Optional: Player zu Beginn an Spawnposition setzen
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
        }
    }

    void Update()
    {
        // Eingabe sammeln (WASD oder Pfeiltasten)
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize(); // gleichmäßige Diagonalgeschwindigkeit
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Sterbeton abspielen
            if (deathSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(deathSound);
            }

            // Respawn mit kurzer Verzögerung
            StartCoroutine(RespawnWithDelay(0.1f));
        }
    }

    private IEnumerator RespawnWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
        }
    }
}
