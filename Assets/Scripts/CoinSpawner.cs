using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public float spawnInterval = 10f; // Time between spawns

    void Start()
    {
        // Start spawning coins repeatedly after an initial delay
        InvokeRepeating("SpawnCoin", 2f, spawnInterval);
    }

    void SpawnCoin()
    {
        // Define the screen boundaries in world coordinates
        // Adjust these values based on your camera size and game world limits
        float screenWidth = Camera.main.orthographicSize * 2f * Camera.main.aspect;
        float screenHeight = Camera.main.orthographicSize * 2f;

        // Generate a random position within screen bounds
        float randomX = Random.Range(-screenWidth / 2f, screenWidth / 2f);
        float randomY = Random.Range(-screenHeight / 2f, screenHeight / 2f);
        Vector2 randomPosition = new Vector2(randomX, randomY);

        // Instantiate the coin prefab at the random position
        Instantiate(coinPrefab, randomPosition, Quaternion.identity);
    }
}

