using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [Header("Sichtfeld")]
    public float viewRadius = 5f;
    public float viewAngle = 45f;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public Transform eyeOrigin;

    [Header("Rotation")]
    public float rotationSpeed = 50f;
    public bool pingPongRotation = true;
    private float angle = 0f;
    private float rotationDirection = 1f;

    [Header("Entdeckung")]
    public Transform spawnPoint;
    public Transform player;

    [Header("Sound")]
    public AudioClip alertSound;
    private AudioSource audioSource;
    private bool hasPlayedAlert = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        HandleRotation();
        DetectPlayer();
    }

    void HandleRotation()
    {
        if (pingPongRotation)
        {
            angle += rotationSpeed * rotationDirection * Time.deltaTime;

            if (angle > 45f || angle < -45f)
                rotationDirection *= -1;

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
    }

    void DetectPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(eyeOrigin.position, viewRadius, playerMask);

        bool playerSeen = false;

        foreach (Collider2D hit in hits)
        {
            Transform target = hit.transform;

            if (target.gameObject.layer == LayerMask.NameToLayer("Hidden"))
                continue;

            Vector2 dirToTarget = (target.position - eyeOrigin.position).normalized;
            float angleToTarget = Vector2.Angle(transform.right, dirToTarget);

            if (angleToTarget < viewAngle / 2)
            {
                float dist = Vector2.Distance(eyeOrigin.position, target.position);
                RaycastHit2D hitInfo = Physics2D.Raycast(eyeOrigin.position, dirToTarget, dist, ~obstacleMask);

                if (hitInfo.collider != null && hitInfo.collider.CompareTag("Player"))
                {
                    playerSeen = true;

                    // Nur einmal spielen pro Entdeckung
                    if (!hasPlayedAlert && alertSound != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(alertSound);
                        hasPlayedAlert = true;
                    }

                    target.position = spawnPoint.position;
                    break;
                }
            }
        }

        // Wenn Spieler nicht mehr gesehen wird → Sound wieder erlauben
        if (!playerSeen)
        {
            hasPlayedAlert = false;
        }
    }

    void OnDrawGizmos()
    {
        if (eyeOrigin == null) eyeOrigin = transform;

        Gizmos.color = new Color(1, 1, 0, 0.3f);
        Vector3 rightLimit = Quaternion.Euler(0, 0, viewAngle / 2) * transform.right;
        Vector3 leftLimit = Quaternion.Euler(0, 0, -viewAngle / 2) * transform.right;

        Gizmos.DrawLine(eyeOrigin.position, eyeOrigin.position + rightLimit * viewRadius);
        Gizmos.DrawLine(eyeOrigin.position, eyeOrigin.position + leftLimit * viewRadius);
        Gizmos.DrawWireSphere(eyeOrigin.position, viewRadius);
    }
}