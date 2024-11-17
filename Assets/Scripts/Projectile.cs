using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 3f; // Tiempo antes de que el proyectil se destruya automáticamente
    public int projectileDamage = 1;

    void Start()
    {
        // Destruir el proyectil después de un tiempo
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si colisiona con un enemigo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Aplicar daño al enemigo
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(1); // Inflige 1 de daño por disparo
            }

            // Destruir el proyectil
            Destroy(gameObject);
        }
    }

}
