using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 3; // Cantidad máxima de disparos que puede recibir
    public float patrolDistance = 3f; // Distancia entre los puntos A y B
    public float speed = 2f; // Velocidad de patrullaje

    private int currentHealth;
    private Vector2 pointA; // Punto A calculado dinámicamente
    private Vector2 pointB; // Punto B calculado dinámicamente
    private Vector2 currentTarget;

    void Start()
    {
        // Inicializar la salud
        currentHealth = maxHealth;

        // Calcular dinámicamente los puntos de patrulla
        pointA = new Vector2(transform.position.x - patrolDistance, transform.position.y);
        pointB = new Vector2(transform.position.x + patrolDistance, transform.position.y);

        // Configurar el objetivo inicial
        currentTarget = pointB;
    }

    void Update()
    {
        // Patrullar entre los puntos si está vivo
        if (currentHealth > 0)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        // Mover hacia el objetivo actual
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        // Cambiar de objetivo al llegar
        if (Vector2.Distance(transform.position, currentTarget) < 0.1f)
        {
            currentTarget = (currentTarget == pointA) ? pointB : pointA;
        }
    }

    public void TakeDamage(int damage)
    {
        // Reducir la salud del enemigo
        currentHealth -= damage;

        // Verificar si la salud llega a cero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Destruir el objeto enemigo
        Destroy(gameObject);

        // Opcional: Añade efectos visuales o sonido aquí
    }
}
