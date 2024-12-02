using TarodevController;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip playerJumpAudio;
    [SerializeField] private AudioClip helperAttackAudio;
    [SerializeField] private AudioClip playerLoseALife;

    [Header("Script References")]
    public PlayerController playerController;
    public PlayerHealth playerHealth;
    public HelperAttack helperAttack;
    public Enemy enemy;


    private void Start()
    {
        playerController.OnPlayerJump += PlayerController_OnJump;
        helperAttack.OnHelperAttack += HelperAttack_OnAttack;
        enemy.OnTakeDamage += Enemy_OnTakeDamage;
        playerHealth.OnLoseLife += PlayerHealth_OnLoseLife;
    }

    private void PlayerController_OnJump()
    {
        PlaySound(playerJumpAudio);
    }

    private void HelperAttack_OnAttack()
    {
        PlaySound(helperAttackAudio);
    }

    private void Enemy_OnTakeDamage()
    {
        //PlaySound();
    }

    private void PlayerHealth_OnLoseLife()
    {
        PlaySound(playerLoseALife);
    }

    private void PlaySound(AudioClip audioClip)
    {
        if (audioSource != null && audioClip != null)
        {
            audioSource.PlayOneShot(audioClip); // Reproduce el sonido del salto
        }
        else
        {
            Debug.LogWarning("AudioSource o audioClip no asignado en el SoundManager.");
        }
    }

}