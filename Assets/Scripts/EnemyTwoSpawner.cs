using UnityEngine;

public class EnemyType2Spawner : MonoBehaviour
{
    public GameObject enemyType2Prefab;
    public float spawnInterval = 3f;
    public float spawnXMin = -8f;
    public float spawnXMax = 8f;
    public float spawnY = 6f;


    void Start()
    {
        InvokeRepeating("SpawnEnemyType2", 2f, spawnInterval);
    }

    void SpawnEnemyType2()
    {
        float xPos = Random.Range(spawnXMin, spawnXMax);
        Vector3 spawnPos = new Vector3(xPos, spawnY, 0f);
        Instantiate(enemyType2Prefab, spawnPos, Quaternion.identity);
    }
}