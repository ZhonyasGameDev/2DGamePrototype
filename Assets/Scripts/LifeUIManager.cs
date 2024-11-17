using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUIManager : MonoBehaviour
{
    public GameObject lifePanel; // Panel que contiene los corazones
    public Sprite fullHeart; // Sprite del corazón lleno
    public Sprite emptyHeart; // Sprite del corazón vacío
    public PlayerHealth playerHealth; // Referencia al script del jugador

    private List<Image> heartImages = new List<Image>();

    private void Start()
    {
        // Inicializar la lista de imágenes de corazones
        foreach (Transform child in lifePanel.transform)
        {
            Image heartImage = child.GetComponent<Image>();
            if (heartImage != null)
            {
                heartImages.Add(heartImage);
            }
        }

        // Asegurarse de que el número de corazones en la UI coincide con las vidas máximas del jugador
        if (heartImages.Count != playerHealth.maxLives)
        {
            Debug.LogWarning("El número de corazones en la UI no coincide con las vidas máximas del jugador.");
        }

        // UpdateLifeUI();
    }

    private void Update()
    {
        UpdateLifeUI();
    }

    public void UpdateLifeUI()
    {
        // Recorrer los corazones y asignar sprites según las vidas actuales del jugador
        for (int i = 0; i < heartImages.Count; i++) // 0, 1, 2
        {
            if (i < playerHealth.CurrentLives) // 0 < 3, 1 < 3, 2 < 3
            {
                heartImages[i].sprite = fullHeart; // Corazón lleno
            }
            else
            {
                heartImages[i].sprite = emptyHeart; // Corazón vacío 
            }

        }
    }


}
