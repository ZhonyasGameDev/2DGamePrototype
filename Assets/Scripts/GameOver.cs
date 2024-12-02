using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;

    private void Start()
    {
        
    }

    private void Show()
    {
        gameOverUI.SetActive(true);
    }

}
