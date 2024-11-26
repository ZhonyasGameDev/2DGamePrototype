using TarodevController;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip jumpSound; // Sonido del salto

    public PlayerController playerController;

    void OnEnable()
    {
        // Suscribir al evento OnPlayerJump
        PlayerController.OnPlayerJump += PlayJumpSound;
    }

    void OnDisable()
    {
        // Desuscribir al evento OnPlayerJump
        PlayerController.OnPlayerJump -= PlayJumpSound;
    }

    void PlayJumpSound()
    {
        if (audioSource != null && jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound); // Reproduce el sonido del salto
        }
        else
        {
            Debug.LogWarning("AudioSource o JumpSound no asignado en el SoundManager.");
        }
    }
}
