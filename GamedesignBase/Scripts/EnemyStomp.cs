using UnityEngine;

public class EnemyStomp : MonoBehaviour
{
    public float bounceForce = 12f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Gegner zerst�ren
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.Die(); // saubere Methode aus Enemy-Skript
            }

            // Spieler nach oben bouncen
            Rigidbody2D rb = GetComponentInParent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceForce);
            }
        }
    }
}
