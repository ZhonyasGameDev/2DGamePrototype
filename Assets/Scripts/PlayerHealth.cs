using System.Collections;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TarodevController;


public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3; // Número máximo de vidas
    public float invulnerabilityTime = 2f; // Tiempo de invulnerabilidad después de recibir daño

    [SerializeField] private int currentLives;
    private bool isInvulnerable = false;

    public event Action OnLoseLife;
    //public event Action OnDie;

    private bool isDie = false;
    

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private PlayerController playerController;



    public LifeUIManager lifeUIManager;

    void Start()
    {
        // Inicializar las vidas
        currentLives = maxLives;
        // Debug.Log(maxLives);

        //lifeUIManager = FindObjectOfType<LifeUIManager>();
    }

    private void Update()
    {
        if (isDie && Input.GetKeyDown(KeyCode.Space))
        {
            LoadScene("GameScene");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si el jugador ha colisionado con un enemigo
        if (collision.gameObject.CompareTag("Enemy") && !isInvulnerable)
        {
            LoseLife();
        }
        else if (collision.gameObject.CompareTag("Heart") && !isDie)
        {
            Debug.Log("HEY!");
            AddLife();
        }

    }

    void LoseLife()
    {
        currentLives--;

        //
        OnLoseLife?.Invoke();

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
        gameOverUI.SetActive(true);
        isDie = true;

        //gameObject.SetActive(false);
        // Opcional: Recargar la escena o mostrar una pantalla de Game Over
        // UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
    }

    public void AddLife()
    {
        // Método para añadir una vida extra si es necesario
        if (currentLives < maxLives)
        {
            Debug.Log("OK!");
            currentLives++;
        }
    }

    public int CurrentLives
    {
        get { return currentLives; }
    }

    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


}
