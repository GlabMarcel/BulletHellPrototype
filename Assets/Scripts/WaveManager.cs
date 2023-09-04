using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array to hold different enemy prefabs
    private int currentWaveIndex = 0; // The current wave number
    private int enemiesAlive = 0; // Number of enemies currently alive

    private int baseEnemies = 5; // Base number of enemies for the first wave
    private float enemyIncreaseFactor = 1.5f; // Factor by which enemies increase each wave

    private void Start()
    {
        StartNextWave();
    }

    private void Update()
    {
        if (enemiesAlive <= 0)
        {
            StartNextWave();
        }
    }

    void StartNextWave()
    {
        Debug.Log("Starting Wave: " + (currentWaveIndex + 1));
        int totalEnemiesThisWave = Mathf.RoundToInt(baseEnemies * Mathf.Pow(enemyIncreaseFactor, currentWaveIndex));
        SpawnEnemies(totalEnemiesThisWave);
        currentWaveIndex++;
    }

    void SpawnEnemies(int totalEnemies)
    {
        Vector2 topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));

        for (int i = 0; i < totalEnemies; i++)
        {
            // Randomly select an enemy prefab from the available prefabs
            GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // Randomly select an edge (Top, Bottom, Left, Right)
            int edge = Random.Range(0, 4);

            Vector3 spawnPosition = Vector3.zero;

            switch (edge)
            {
                case 0: // Top
                    spawnPosition = new Vector3(Random.Range(bottomLeft.x, topRight.x), topRight.y, 0);
                    break;
                case 1: // Bottom
                    spawnPosition = new Vector3(Random.Range(bottomLeft.x, topRight.x), bottomLeft.y, 0);
                    break;
                case 2: // Left
                    spawnPosition = new Vector3(bottomLeft.x, Random.Range(bottomLeft.y, topRight.y), 0);
                    break;
                case 3: // Right
                    spawnPosition = new Vector3(topRight.x, Random.Range(bottomLeft.y, topRight.y), 0);
                    break;
            }

            Instantiate(randomEnemyPrefab, spawnPosition, Quaternion.identity);
            enemiesAlive++;
        }
    }


    public void EnemyDied()
    {
        enemiesAlive--;
        Debug.Log("Enemies Alive: " + enemiesAlive);
    }
}
