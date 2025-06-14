using UnityEngine;

public class AusweichenCameraFollow : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float stopPositionX = 50f;
    public Transform respawnPoint; // Setzbar im Editor

    private bool isMoving = false;

    void Update()
    {
        if (isMoving)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            if (transform.position.x >= stopPositionX)
            {
                isMoving = false;
            }
        }
    }

    public void StartMoving()
    {
        if (respawnPoint != null)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = respawnPoint.position.x;
            newPosition.y = respawnPoint.position.y;
            transform.position = newPosition;
        }

        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
    }
}
