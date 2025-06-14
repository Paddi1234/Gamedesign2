using UnityEngine;
using TMPro;

public class FlyingController : MonoBehaviour
{
    public float flapForce = 5f;
    public float forwardSpeed = 2f;
    public AudioClip deathSound;
    public Transform respawnPoint;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    private bool isDead = false;
    private AusweichenCameraFollow cameraScript;
    private Animator animator;
    public GameObject respawnHintUI;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        cameraScript = Camera.main.GetComponent<AusweichenCameraFollow>();
        Respawn(); // <-- Direkt beim Spielstart auslösen

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead)
        {
            if (respawnHintUI != null)
                respawnHintUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.R))
            {
                Respawn();
            }
        }
        else
        {
            if (respawnHintUI != null)
                respawnHintUI.SetActive(false);
        }

        // Spieler fliegt bei Eingabe
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * flapForce, ForceMode2D.Impulse);

            if (animator != null)
            {
                animator.SetTrigger("Flap");
            }
        }
    }

    void FixedUpdate()
    {
        if (isDead) return;

        // Ständiger Vorwärtsflug
        rb.linearVelocity = new Vector2(forwardSpeed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        isDead = true;
        rb.linearVelocity = Vector2.zero;

        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        Debug.Log("Spieler tot – Drücke 'R' zum Respawn");
    }

    void Respawn()
    {
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
        }

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0;
        isDead = false;

        if (cameraScript != null)
        {
            cameraScript.StartMoving(); // Kamera starten
        }

        Debug.Log("Respawn erfolgt");
    }
}


