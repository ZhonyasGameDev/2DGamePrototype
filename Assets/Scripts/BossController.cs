using System.Collections;
using TarodevController;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Stats del jefe")]
    public int maxHealth = 20; // Vida máxima
    private int currentHealth; // Vida actual
    public int damage = 1; // Daño que inflige el jefe
    public float moveSpeed = 2f; // Velocidad de movimiento
    public float detectionRange = 10f; // Rango de detección del jugador

    [Header("Ataques")]
    public GameObject projectile; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde dispara
    public float attackCooldown = 2f; // Tiempo entre ataques

    private Transform player; // Referencia al jugador
    private bool isAttacking = false; // Estado de ataque
    private bool isDead = false; // Estado de muerte

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isDead) return;

        if (player != null && Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            FollowPlayer();
            if (!isAttacking)
            {
                StartCoroutine(Attack());
            }
        }
    }

    void FollowPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }


    IEnumerator Attack()
    {
        isAttacking = true;

        if (projectile != null && firePoint != null)
        {
            GameObject newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
            BossProjectile projectileScript = newProjectile.GetComponent<BossProjectile>();

            if (projectileScript != null)
            {
                Vector2 direction = (player.position - firePoint.position).normalized; // Dirección hacia el jugador
                projectileScript.SetDirection(direction);
            }
        }

        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    /*IEnumerator Attack()
    {
        isAttacking = true;

        // Animación o efectos de ataque
        if (projectile != null && firePoint != null)
        {
            Instantiate(projectile, firePoint.position, firePoint.rotation);
        }

        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }*/

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        // Animación o efectos de muerte
        Debug.Log("¡El jefe ha sido derrotado!");
        Destroy(gameObject, 1f); // Destruir el objeto tras 1 segundo
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Inflige daño al jugador (asumiendo que el jugador tiene un script con un método TakeDamage)
            collision.gameObject.GetComponent<PlayerController>()?.TakeDamage(damage);
        }
    }*/
}
