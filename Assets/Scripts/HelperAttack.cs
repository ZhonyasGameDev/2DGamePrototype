using UnityEngine;
using System;
public class HelperAttack : MonoBehaviour
{
    public float attackRange = 5f; // Rango de ataque del ayudante
    public float attackCooldown = 1f; // Tiempo entre disparos
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se dispara el proyectil
    private float attackTimer = 0f;

    // public event EventHandler OnHelperAttack; // Evento 
    public event Action OnHelperAttack; // Evento 

    public Animator animator;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Si el jugador muere, no ataca
        if (PlayerHealth.isDie) return;

        attackTimer -= Time.deltaTime;

        // Buscar enemigos dentro del rango
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("Enemy"));

        if (enemiesInRange.Length > 0)
        {
            //spriteRenderer.color = Color.red;
            spriteRenderer.color = ColorUtility.TryParseHtmlString("#FF7C86", out Color newColor) ? newColor : Color.white;

            if (attackTimer <= 0f && Input.GetMouseButtonDown(0))
            {
                // Disparar al primer enemigo en el rango
                Shoot(enemiesInRange[0].transform);
                attackTimer = attackCooldown;
            }
            else
            {
                animator.SetBool("IsAttack", false);
            }
        }
        else
        {
            //animator.SetBool("IsAttack", false);
            spriteRenderer.color = Color.white;
        }

        /*
        // Comprobar si hay enemigos en rango y si se presionó la tecla de ataque
        if (enemiesInRange.Length > 0 && attackTimer <= 0f && Input.GetMouseButtonDown(0))
        {
            // Disparar al primer enemigo en el rango
            Shoot(enemiesInRange[0].transform);
            attackTimer = attackCooldown;
        }
        else
        {
            animator.SetBool("IsAttack", false);
        }
        */

    }

    void Shoot(Transform target)
    {
        if (projectilePrefab != null && firePoint != null)
        {
            // Crear el proyectil
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            Vector2 direction = (target.position - firePoint.position).normalized;

            // Obtener el SpriteRenderer del proyectil
            SpriteRenderer projectileSprite = projectile.GetComponent<SpriteRenderer>();
            if (projectileSprite != null)
            {
                // Cambiar la propiedad Flip en función de la dirección
                if (direction.x > 0)
                {
                    // Si el proyectil se dirige hacia la derecha
                    projectileSprite.flipX = true;
                }
                else
                {
                    // Si el proyectil se dirige hacia la izquierda
                    projectileSprite.flipX = false;
                }
            }


            // Apuntar al objetivo
            //Vector2 direction = (target.position - firePoint.position).normalized;
            projectile.GetComponent<Rigidbody2D>().linearVelocity = direction * 10f; // Velocidad del proyectil

            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //projectile.transform.rotation = Quaternion.Euler(0, 0, angle);


            animator.SetBool("IsAttack", true);
            OnHelperAttack?.Invoke();
        }
    }

    void OnDrawGizmosSelected()
    {
        // Dibujar el rango de ataque en el editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
