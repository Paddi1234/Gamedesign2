using UnityEngine;

public class SidekickController : MonoBehaviour
{
    public Transform player;
    public float followDistance = 1.5f;
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private Animator animator;

    private bool isGrounded;
    private bool hasJumpedWithPlayer;

    private Vector3 lastPlayerPosition;   // Zum Erkennen von Respawn

    private float jumpCooldown = 0.1f;
    private float jumpTimer = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player != null)
            lastPlayerPosition = player.position;
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        // === 1. Prüfen auf Respawn (teleportation) ===
        float teleportThreshold = 1.5f; // Distanz, die nicht durch normale Bewegung entstehen kann
        if (Vector3.Distance(player.position, lastPlayerPosition) > teleportThreshold)
        {
            // Spieler wurde wahrscheinlich teleportiert → Sidekick mitnehmen
            transform.position = player.position + new Vector3(-followDistance, 0, 0);
            rb.linearVelocity = Vector2.zero;
        }

        lastPlayerPosition = player.position;

        // === 2. Bodencheck ===
        jumpTimer -= Time.fixedDeltaTime;
        if (jumpTimer <= 0f)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }

        // === 3. Bewegung ===
        float targetX = player.position.x - Mathf.Sign(player.localScale.x) * followDistance;
        float distanceX = targetX - transform.position.x;
        float direction = Mathf.Sign(distanceX);

        if (Mathf.Abs(distanceX) > 0.1f)
            rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

        // === 4. Springen ===
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            if (playerController.IsJumping && isGrounded && !hasJumpedWithPlayer)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                hasJumpedWithPlayer = true;
                jumpTimer = jumpCooldown;
            }

            if (!playerController.IsJumping && isGrounded)
                hasJumpedWithPlayer = false;
        }

        // === 5. Flip ===
        if (direction != 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * Mathf.Sign(direction), transform.localScale.y, transform.localScale.z);
        }

        // === 6. Animationen ===
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        animator.SetBool("IsJumping", !isGrounded && rb.linearVelocity.y > 0.1f);
        animator.SetBool("IsFalling", !isGrounded && rb.linearVelocity.y < -0.1f);
        animator.SetBool("IsGrounded", isGrounded);
    }
}







