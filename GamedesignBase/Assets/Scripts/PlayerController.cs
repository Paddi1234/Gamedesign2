using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRayLength = 0.1f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    public Animator Anim { get; private set; }  // öffentliche Property

    private Vector3 originalScale;

    public bool IsJumping { get; private set; }
    public bool IsGrounded { get; private set; }
    public bool IsFalling { get; private set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        IsGrounded = CheckGrounded();

        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            IsJumping = true;
        }

        if (IsGrounded)
        {
            IsJumping = false;
            IsFalling = false;
        }
        else
        {
            if (rb.linearVelocity.y > 0.1f)
            {
                IsJumping = true;
                IsFalling = false;
            }
            else if (rb.linearVelocity.y < -0.1f)
            {
                IsFalling = true;
                IsJumping = false;
            }
        }

        Anim.SetFloat("Speed", Mathf.Abs(moveInput));
        Anim.SetBool("IsJumping", IsJumping);
        Anim.SetBool("IsFalling", IsFalling);
        Anim.SetBool("IsGrounded", IsGrounded);

        // Flip
        if (moveInput > 0)
            transform.localScale = originalScale;
        else if (moveInput < 0)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
    }

    private bool CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckRayLength, groundLayer);
        Debug.DrawRay(groundCheck.position, Vector2.down * groundCheckRayLength, Color.red);
        return hit.collider != null;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckRayLength);
    }
}



















