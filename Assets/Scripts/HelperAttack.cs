using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperAttack : MonoBehaviour
{
    public float attackRange = 5f; // Rango de ataque del ayudante
    public float attackCooldown = 1f; // Tiempo entre disparos
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se dispara el proyectil
    public KeyCode attackKey = KeyCode.Space; // Tecla para disparar

    private float attackTimer = 0f;

    void Update()
    {
        attackTimer -= Time.deltaTime;

        // Buscar enemigos dentro del rango
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("Enemy"));

        // Comprobar si hay enemigos en rango y si se presionó la tecla de ataque
        if (enemiesInRange.Length > 0 && attackTimer <= 0f && Input.GetMouseButtonDown(0))
        {
            // Disparar al primer enemigo en el rango
            Shoot(enemiesInRange[0].transform);
            attackTimer = attackCooldown;
        }
    }

    void Shoot(Transform target)
    {
        if (projectilePrefab != null && firePoint != null)
        {
            // Crear el proyectil
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Apuntar al objetivo
            Vector2 direction = (target.position - firePoint.position).normalized;
            projectile.GetComponent<Rigidbody2D>().linearVelocity = direction * 10f; // Velocidad del proyectil
        }
    }

    void OnDrawGizmosSelected()
    {
        // Dibujar el rango de ataque en el editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
