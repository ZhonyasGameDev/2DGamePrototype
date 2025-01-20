using System.Collections;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform[] firePoints; // Puntos desde donde se disparan los proyectiles
    public float fireRate = 2f; // Intervalo de tiempo entre disparos
    public float projectileSpeed = 5f; // Velocidad de los proyectiles
    public Transform player; // Referencia al jugador

    private float fireTimer;

    private void Start()
    {
        fireTimer = 0f;
    }

    private void Update()
    {
        // Si el Player muere, no dispara
        if (PlayerHealth.isDie) return;

        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0)
        {
            FireRandomPattern();
            fireTimer = fireRate;
        }
    }

    private void FireRandomPattern()
    {
        int pattern = Random.Range(0, 3); // Selecciona un patrón aleatorio
        switch (pattern)
        {
            case 0:
                FireSingleShot(firePoints[0]);
                break;
            case 1:
                FireMultipleDirections(firePoints[1]); 
                break;
            case 2:
                StartCoroutine(FireBurstCoroutine(firePoints[2])); 
                break;
        }
    }

    private void FireSingleShot(Transform firePoint)
    {
        // Instancia el proyectil en la posición actual del firePoint
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Calcula la dirección hacia el jugador
        Vector2 direction = (player.position - firePoint.position).normalized;

        // Asigna una velocidad al proyectil
        float singleShotSpeed = projectileSpeed * 2f; // Ejemplo de modificación local
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * singleShotSpeed;
        }

        // Ajusta la rotación del proyectil para que apunte en la dirección del movimiento
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void FireMultipleDirections(Transform firePoint)
    {
        int numberOfProjectiles = 8; // Número de proyectiles a disparar en un círculo
        float angleStep = 360f / numberOfProjectiles; // Ángulo entre cada proyectil
        float angle = 0f;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            // Calcula la dirección del proyectil
            float projectileDirX = firePoint.position.x + Mathf.Sin((angle * Mathf.PI) / 180);
            float projectileDirY = firePoint.position.y + Mathf.Cos((angle * Mathf.PI) / 180);

            Vector2 projectileVector = new Vector2(projectileDirX, projectileDirY);
            Vector2 projectileMoveDirection = (projectileVector - (Vector2)firePoint.position).normalized;

            // Instancia el proyectil en la posición actual del firePoint
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Asigna una velocidad al proyectil
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = new Vector2(projectileMoveDirection.x * projectileSpeed, projectileMoveDirection.y * projectileSpeed);
            }

            // Ajusta la rotación del proyectil para que apunte en la dirección del movimiento
            float projectileAngle = Mathf.Atan2(projectileMoveDirection.y, projectileMoveDirection.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, projectileAngle));

            angle += angleStep;
        }
    }

    private void FireBurst(Transform firePoint)
    {
        StartCoroutine(FireBurstCoroutine(firePoint));
    }

    private IEnumerator FireBurstCoroutine(Transform firePoint)
    {
        int bulletNumber = Random.Range(3, 7); // Selecciona un numero aleatorio de proyectiles

        for (int i = 0; i < bulletNumber; i++) // Dispara una ráfaga de un numero aleatorio de proyectiles
        {
            FireSingleShot(firePoint);
            yield return new WaitForSeconds(0.2f); // Intervalo entre disparos en la ráfaga
        }
    }
}