using UnityEngine;

public class CameraFollowLimiter : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float minX; // Posición mínima en el eje X para la cámara

    void LateUpdate()
    {
        if (player != null)
        {
            // Calcula la nueva posición de la cámara, pero limita su movimiento hacia la izquierda
            float targetX = Mathf.Max(player.position.x, minX);

            // Actualiza la posición de la cámara manteniendo el eje Y y Z
            transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
        }
    }
}
