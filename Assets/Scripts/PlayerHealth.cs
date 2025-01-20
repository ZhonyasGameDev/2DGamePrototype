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
    public event Action OnAddLife;
    public event Action OnIsDie;
    //public event Action OnDie;

    public static bool isDie;


    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject player;



    public LifeUIManager lifeUIManager;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        // Inicializar las vidas
        currentLives = maxLives;
        isDie = false;
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
        if (isDie) return;

        // Verificar si el jugador ha colisionado con un enemigo
        if (collision.gameObject.CompareTag("Enemy") && !isInvulnerable)
        {
            LoseLife();
        }
        else if (collision.gameObject.CompareTag("Projectile") && !isInvulnerable)
        {
            LoseLife();
            Destroy(collision.gameObject); // Destruir el proyectil
        }
        else if (collision.gameObject.CompareTag("Heart") && !isDie)
        {
            if (currentLives < maxLives)
            {
                AddLife();
                Destroy(collision.gameObject); // Destruir siempre el corazón   
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (isDie) return;

        // Verificar si el jugador sigue en contacto con un enemigo
        if (collision.gameObject.CompareTag("Enemy") && !isInvulnerable)
        {
            LoseLife();
            //damageTimer = damageInterval; // Reinicia el temporizador de daño
        }
    }

    public void LoseLife()
    {
        currentLives--;
        OnLoseLife?.Invoke(); // Event!

        animator.SetTrigger("TakeDamage");
        animator.SetBool("IsJumping", false);

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
        /*
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
        */

        // Mantener el comportamiento de invulnerabilidad del jugador sin alternar su visibilidad
        isInvulnerable = true;

        // Espera durante el tiempo de invulnerabilidad sin alternar visibilidad
        yield return new WaitForSeconds(invulnerabilityTime);

        isInvulnerable = false;
    }


    void GameOver()
    {
        // Lógica para el final del juego
        Debug.Log("Game Over!");
        gameOverUI.SetActive(true);
        isDie = true;

        // Dispara la animacion de muerte
        animator.SetBool("IsDie", true);


        OnIsDie?.Invoke();

        //playerController.enabled = false;
        //player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        player.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;


        //gameObject.SetActive(false);
        // Opcional: Recargar la escena o mostrar una pantalla de Game Over
        // UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
    }

    public void AddLife()
    {
        // Método para añadir una vida extra si es necesario
        if (currentLives < maxLives)
        {
            //Debug.Log("OK!");
            currentLives++;

            OnAddLife?.Invoke();
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
