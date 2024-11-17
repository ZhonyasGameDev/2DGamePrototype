using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3; // Número máximo de vidas
    public float invulnerabilityTime = 2f; // Tiempo de invulnerabilidad después de recibir daño

    private int currentLives;
    private bool isInvulnerable = false;

    public LifeUIManager lifeUIManager;

    void Start()
    {
        // Inicializar las vidas
        currentLives = maxLives;
        // Debug.Log(maxLives);

        //lifeUIManager = FindObjectOfType<LifeUIManager>();

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si el jugador ha colisionado con un enemigo
        if (collision.gameObject.CompareTag("Enemy") && !isInvulnerable)
        {
            LoseLife();
        }
    }

    void LoseLife()
    {
        currentLives--;

        if (currentLives <= 0)
        {
            // lifeUIManager.UpdateLifeUI(); // Nos asegura que se actualice la ultima vida
            GameOver();
        }
        else
        {
            // lifeUIManager.UpdateLifeUI();
            StartCoroutine(InvulnerabilityCoroutine());
        }
    }

    IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;

        // Opcional: Cambiar la apariencia del jugador durante la invulnerabilidad (por ejemplo, parpadeo)
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            for (float i = 0; i < invulnerabilityTime; i += 0.2f)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled; // Alternar visibilidad
                yield return new WaitForSeconds(0.2f);
            }
            spriteRenderer.enabled = true;
        }

        isInvulnerable = false;
    }

    void GameOver()
    {
        // Lógica para el final del juego
        Debug.Log("Game Over!");
        // Opcional: Recargar la escena o mostrar una pantalla de Game Over
        // UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
    }

    public void AddLife()
    {
        // Método para añadir una vida extra si es necesario
        if (currentLives < maxLives)
        {
            currentLives++;
        }
    }

    public int CurrentLives
    {
        get { return currentLives; }
    }

}
