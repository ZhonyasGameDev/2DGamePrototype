using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target; // El objeto que será seguido
    public float followSpeed = 5f; // Velocidad de seguimiento
    public float repositionSpeed = 3f; // Velocidad de reposicionamiento
    public float distanceBehind = 2f; // Distancia detrás del objeto a seguir
    public float stopThreshold = 0.1f; // Umbral de velocidad para determinar si el objeto a seguir está detenido

    public Vector3 relativePosition; // Posición relativa del seguidor con respecto al objeto a seguir

    private Vector3 lastPosition; // Última posición del objeto a seguir
    private bool isRepositioning = false; // Si el seguidor necesita reposicionarse
    private Vector3 targetRepositionPosition; // La posición hacia la que el seguidor debe moverse cuando se detiene

    private bool isFacingRight = true; // Para saber si el seguidor está mirando a la derecha o a la izquierda
    private bool hasFlipped = false; // Para asegurarnos de que el flip solo ocurra una vez

    void Update()
    {
        // Verifica si el objeto a seguir se ha detenido (velocidad muy baja)
        if (Vector3.Distance(target.position, lastPosition) < stopThreshold)
        {
            if (!isRepositioning)
            {
                isRepositioning = true;
                RepositionFollower();
            }
        }
        else
        {
            isRepositioning = false;
        }

        // Mueve el seguidor hacia la posición relativa del objeto a seguir
        if (!isRepositioning)
        {
            FollowTarget();
        }
        else
        {
            // Suaviza el movimiento del seguidor al reposicionarse detrás del objeto a seguir
            SmoothReposition();
        }

        // Verifica si el objeto a seguir ha cambiado de dirección
        if (target.position.x > lastPosition.x && !isFacingRight && !hasFlipped)
        {
            isFacingRight = true;
            FlipFollower(); // Realiza el flip para mirar a la derecha
            hasFlipped = true; // Marca que ya se ha realizado el flip
        }
        else if (target.position.x < lastPosition.x && isFacingRight && !hasFlipped)
        {
            isFacingRight = false;
            FlipFollower(); // Realiza el flip para mirar a la izquierda
            hasFlipped = true; // Marca que ya se ha realizado el flip
        }

        // Si el objeto sigue en movimiento, permitimos que el flip se "resetee" para posibles futuros cambios de dirección
        if (target.position.x != lastPosition.x)
        {
            hasFlipped = false;
        }

        lastPosition = target.position;
    }

    void FollowTarget()
    {
        // Mueve al seguidor hacia la posición calculada con el offset relativo
        Vector3 targetPosition = target.position + relativePosition;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    void RepositionFollower()
    {
        // Reposiciona al seguidor detrás del objeto a seguir según su dirección actual
        Vector3 direction = isFacingRight ? Vector3.left : Vector3.right;
        targetRepositionPosition = target.position + direction * distanceBehind + relativePosition;
    }

    void SmoothReposition()
    {
        // Suaviza el desplazamiento del seguidor hacia la nueva posición detrás del objeto
        transform.position = Vector3.Lerp(transform.position, targetRepositionPosition, repositionSpeed * Time.deltaTime);
    }

    void FlipFollower()
    {
        // Invierte el valor de la escala en el eje X para hacer el flip
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (isFacingRight ? 1 : -1);
        transform.localScale = scale;
    }
}
