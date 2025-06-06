using UnityEngine;

public class LabyrinthController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform spawnPoint; // Wird im Editor gesetzt

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Spieler an definierten Spawnpunkt setzen
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
        }
    }

    void Update()
    {
        // Eingaben abfragen (WASD oder Pfeiltasten)
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        moveVelocity = moveInput * moveSpeed;
    }

    void FixedUpdate()
    {
        // Bewegung anwenden
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Wenn ein Gegner getroffen wird, zurück zum Spawnpunkt
        if (collision.CompareTag("Enemy") && spawnPoint != null)
        {
            transform.position = spawnPoint.position;
        }
    }
}
