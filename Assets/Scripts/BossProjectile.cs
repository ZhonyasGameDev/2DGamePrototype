using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float lifeTime = 5f; // Tiempo de vida del proyectil

    private void Start()
    {
        Destroy(gameObject, lifeTime); // Destruir el proyectil después de un tiempo
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

        if (collision.CompareTag("Player") && !PlayerHealth.isDie)
        {
            // Lógica para dañar al jugador
            playerHealth.LoseLife();
            Destroy(gameObject); // Destruir el proyectil al colisionar con el jugador
        }
    }
    */
}