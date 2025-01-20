using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public Slider healthBarPrefab; // Prefab de la barra de vida
    private Slider healthBarInstance; // Instancia de la barra de vida
    public Canvas mainCanvas; // Referencia al Canvas existente

    void Start()
    {
        currentHealth = maxHealth;

        // Instanciar la barra de vida y configurarla
        if (mainCanvas != null && healthBarPrefab != null)
        {
            healthBarInstance = Instantiate(healthBarPrefab, mainCanvas.transform);
            UpdateHealthBar();

        }
    }

    void Update()
    {
        // Si hay una barra de vida, seguir al enemigo
        if (healthBarInstance != null)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 6f);
            healthBarInstance.transform.position = screenPosition;

            // Mantener la rotación fija
            healthBarInstance.transform.rotation = Quaternion.identity;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarInstance != null)
        {
            healthBarInstance.value = currentHealth / maxHealth;
        }
    }

    void Die()
    {
        // Destruir la barra de vida y el enemigo
        if (healthBarInstance != null)
        {
            Destroy(healthBarInstance.gameObject);
        }
        Destroy(gameObject);
    }
}
