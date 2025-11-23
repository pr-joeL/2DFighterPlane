using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public enum PowerUpType {Shield} 
    public PowerUpType powerUpType;
    public float duration = 5f; // Duration for temporary power-ups
    public float effectAmount = 10f; // Amount of effect (e.g., speed increase, health restored)
    public AudioClip collectedClip;
    public AudioSource audioSource; 
    private void OnTriggerEnter(Collider other) // For 3D collisions
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            ApplyPowerUpEffect(other.gameObject);
            Destroy(gameObject); // Destroy the power-up after collection
        }
    }

    // For 2D collisions, use OnTriggerEnter2D
    private void OnTriggerEnter2D(Collider2D other) // For 2D collisions
    {
        if (other.CompareTag("Player"))
        {
            ApplyPowerUpEffect(other.gameObject);
            Destroy(gameObject);
          
            if (audioSource != null && collectedClip != null)
            {
                audioSource.PlayOneShot(collectedClip);
            }

            Destroy(gameObject);
        }
    }

    void ApplyPowerUpEffect(GameObject player)
    {
        // Get the player's script that handles power-up effects
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.ActivatePowerUp(powerUpType, duration, effectAmount);
        }
    }
}