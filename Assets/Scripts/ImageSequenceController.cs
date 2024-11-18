using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ImageSequenceWithTransitions : MonoBehaviour
{
    private const string SCENE_TO_LOAD = "GameScene";

    public enum SequenceState
    {
        None,
        FadingIn,
        Displaying,
        FadingOut,
        Finished
    }

    [Header("Configuración")]
    public Image[] images; // Lista de imágenes a mostrar en secuencia.
    public float displayTime = 2f; // Tiempo que cada imagen estará completamente visible.
    public float transitionTime = 1f; // Tiempo de la transición (desvanecimiento).

    [Header("UI")]
    public Button finalButton; // Botón que se activará al final.

    private int currentImageIndex = 0; // Índice de la imagen actual.
    private float timer = 0f; // Temporizador general.
    private SequenceState state = SequenceState.None; // Estado actual de la secuencia.

    void Start()
    {
        // Asegurarse de que todas las imágenes y el botón estén desactivados al inicio.
        foreach (Image img in images)
        {
            img.gameObject.SetActive(false);
            SetImageAlpha(img, 0); // Hacerlas transparentes al inicio.
            img.color = new Color(1, 1, 1, img.color.a); // Asegurar color blanco base.
        }


        finalButton.gameObject.SetActive(false);

        // Activar la primera imagen.
        if (images.Length > 0)
        {
            images[0].gameObject.SetActive(true);
            SetImageAlpha(images[0], 1); // Asegurarse de que la primera imagen sea visible.
            state = SequenceState.FadingIn;
        }
    }

    void Update()
    {
        // Manejar los estados de la secuencia.
        switch (state)
        {
            case SequenceState.FadingIn:
                HandleFadeIn();
                break;
            case SequenceState.Displaying:
                HandleDisplay();
                break;
            case SequenceState.FadingOut:
                HandleFadeOut();
                break;
            case SequenceState.Finished:
                // No hacer nada.
                break;
        }
    }

    private void HandleFadeIn()
    {
        if (currentImageIndex < images.Length)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / transitionTime);
            SetImageAlpha(images[currentImageIndex], alpha);

            if (timer >= transitionTime)
            {
                timer = 0f;
                state = SequenceState.Displaying;
            }
        }
    }

    private void HandleDisplay()
    {
        timer += Time.deltaTime;

        if (timer >= displayTime)
        {
            timer = 0f;

            // Si es la última imagen, no hay desvanecimiento.
            if (currentImageIndex == images.Length - 1)
            {
                state = SequenceState.Finished;
                finalButton.gameObject.SetActive(true);
            }
            else
            {
                state = SequenceState.FadingOut;
            }
        }
    }

    private void HandleFadeOut()
    {
        if (currentImageIndex < images.Length)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - (timer / transitionTime));
            SetImageAlpha(images[currentImageIndex], alpha);

            if (timer >= transitionTime)
            {
                timer = 0f;

                // Desactivar la imagen actual y pasar a la siguiente.
                images[currentImageIndex].gameObject.SetActive(false);
                currentImageIndex++;

                // Activar la siguiente imagen si existe.
                if (currentImageIndex < images.Length)
                {
                    images[currentImageIndex].gameObject.SetActive(true);
                    SetImageAlpha(images[currentImageIndex], 0); // Asegurar que la nueva imagen comience desde alfa 0.
                    state = SequenceState.FadingIn;
                }
                else
                {
                    state = SequenceState.Finished;
                    finalButton.gameObject.SetActive(true);
                }
            }
        }
    }

    private void SetImageAlpha(Image image, float alpha)
    {
        if (image != null)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }

    //
    public void NextButton()
    {
        SceneManager.LoadScene(SCENE_TO_LOAD);
    }
}
