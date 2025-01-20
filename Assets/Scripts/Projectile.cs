using System;
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
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }

            if (enemyHealth != null)
            {
                Debug.Log("Hey!");
                enemyHealth.TakeDamage(projectileDamage);
            }
        

            // Destruir el proyectil
            Destroy(gameObject);
        }
    }

}
