using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [Header("Puntos de Movimiento")]
    public Transform[] movePoints; // Array de puntos predefinidos
    public float moveSpeed = 5f; // Velocidad de movimiento

    [Header("Tiempo de Espera entre Movimientos")]
    public float minWaitTime = 1f; // Tiempo mínimo de espera
    public float maxWaitTime = 3f; // Tiempo máximo de espera

    private Rigidbody2D rb; // Referencia al Rigidbody2D del boss
    private Transform targetPoint; // Punto objetivo actual

    private enum BossState { Idle, Moving, Waiting }
    private BossState currentState = BossState.Idle;

    private float waitTimer = 0f; // Temporizador para la espera

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (movePoints.Length < 3)
        {
            Debug.LogError("Debes asignar al menos 3 puntos de movimiento en el inspector.");
            return;
        }

        // Inicializa el estado y selecciona un punto inicial
        SelectNewTarget();
    }

    void Update()
    {
        // Si el jugador muere, no se mueve
        if (PlayerHealth.isDie)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        switch (currentState)
        {
            case BossState.Idle:
                SelectNewTarget();
                break;

            case BossState.Moving:
                MoveToTarget();
                break;

            case BossState.Waiting:
                WaitBeforeNextMove();
                break;
        }
    }

    private void SelectNewTarget()
    {
        Debug.Log("Seleccionando nuevo punto objetivo...");

        // Selecciona un punto aleatorio distinto al actual
        Transform newTarget;
        do
        {
            newTarget = movePoints[Random.Range(0, movePoints.Length)];
        } while (newTarget == targetPoint);

        targetPoint = newTarget;
        currentState = BossState.Moving;
    }

    private void MoveToTarget()
    {
        Debug.Log("Moviendo hacia el punto objetivo...");

        // Calcula la dirección hacia el punto objetivo
        Vector2 direction = (targetPoint.position - transform.position).normalized;

        // Mueve el boss utilizando Rigidbody2D
        rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

        // Si alcanza el punto objetivo, cambia al estado de espera
        if (Mathf.Abs(transform.position.x - targetPoint.position.x) < 0.1f)
        {
            Debug.Log("Llego al punto objetivo!");
            rb.linearVelocity = Vector2.zero; // Detener el movimiento
            currentState = BossState.Waiting;
            waitTimer = Random.Range(minWaitTime, maxWaitTime); // Define un tiempo aleatorio de espera
        }
    }

    private void WaitBeforeNextMove()
    {

        rb.linearVelocity = Vector2.zero; // Detener el movimiento
        Debug.Log("Esperando antes de seleccionar un nuevo punto...");

        // Reduce el temporizador de espera
        waitTimer -= Time.deltaTime;

        // Si el temporizador llega a cero, cambia al estado Idle para seleccionar un nuevo punto
        if (waitTimer <= 0f)
        {
            currentState = BossState.Idle;
        }
    }

    private void OnDrawGizmos()
    {
        // Dibuja los puntos de movimiento en la escena para referencia
        Gizmos.color = Color.green;

        if (movePoints != null)
        {
            foreach (var point in movePoints)
            {
                if (point != null)
                    Gizmos.DrawSphere(point.position, 0.2f);
            }
        }
    }
}
