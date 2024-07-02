using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;  // Default coin value, you can change this in the Inspector
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private Collider2D collider2D;
    private bool isCollected = false;

    void Start()
    {
        // Get the AudioSource, SpriteRenderer, and Collider2D components
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that collided with the coin is the player
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true; // Ensure this block only runs once

            // Add to the player's score
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.CollectCoin(coinValue);
            }

            // Play the coin collection sound
            if (audioSource != null)
            {
                audioSource.Play();
            }

            // Disable the sprite renderer and collider to make the coin disappear
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }
            if (collider2D != null)
            {
                collider2D.enabled = false;
            }

            // Destroy the coin object after the sound has finished playing
            Destroy(gameObject, audioSource.clip.length);
        }
    }

    // Ensure the coinValue is set to 1 in the Inspector
    void Reset()
    {
        coinValue = 1;
    }
}


