using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject pauseMenu;

    private bool juegoEnPausa = false;

    void Update()
    {
        // Detectar si el jugador presiona la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AlternarPausa();
        }
    }

    // Método para alternar entre pausar y reanudar el juego
    public void AlternarPausa()
    {
        if (juegoEnPausa)
        {
            ReanudarJuego();
        }
        else
        {
            Pausar();
        }
    }

    // Método para pausar el juego
    private void Pausar()
    {
        Time.timeScale = 0f; // Detiene el tiempo del juego
        juegoEnPausa = true;
        // Opcional: Mostrar un menú de pausa
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);


        Debug.Log("Juego en pausa");
    }

    // Método para reanudar el juego
    private void ReanudarJuego()
    {
        Time.timeScale = 1f; // Restaura el tiempo normal
        juegoEnPausa = false;
        // Opcional: Ocultar el menú de pausa
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);


        Debug.Log("Juego reanudado");
    }

}
