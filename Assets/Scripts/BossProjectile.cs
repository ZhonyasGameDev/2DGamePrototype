using TarodevController;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    [Header("Configuraci�n del proyectil")]
    public float speed = 5f; // Velocidad del proyectil
    public int damage = 10; // Da�o que inflige el proyectil
    public float lifetime = 5f; // Tiempo antes de que el proyectil desaparezca

    private Vector2 direction;

    void Start()
    {
        // Destruir el proyectil autom�ticamente despu�s del tiempo de vida
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Mover el proyectil
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized; // Asegurarse de que la direcci�n est� normalizada
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el proyectil impacta al jugador
        if (collision.CompareTag("Player"))
        {
            // Inflige da�o al jugador (aseg�rate de que el jugador tenga un m�todo TakeDamage)
            //collision.GetComponent<PlayerController>()?.TakeDamage(damage);

            // Destruir el proyectil al impactar
            Destroy(gameObject);
        }

        // Opcional: Destruir el proyectil si impacta contra otro objeto
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
