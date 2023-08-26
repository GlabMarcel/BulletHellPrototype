using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public WavePattern[] patterns;

        [System.Serializable]
        public class WavePattern
        {
            public GameObject enemyPrefab;
            public int count;
            public string patternType;
        }
    }

    public Wave[] waves;
    public GameObject[] enemyPrefabs; // Array to hold different enemy prefabs

    private int currentWaveIndex = 0;
    private int enemiesAlive = 0;

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
    Debug.Log("Starting Wave: " + (currentWaveIndex + 1)); // Print the current wave number

    int totalEnemiesThisWave = Mathf.RoundToInt(baseEnemies * Mathf.Pow(enemyIncreaseFactor, currentWaveIndex));
    GenerateRandomPatterns(totalEnemiesThisWave);

    currentWaveIndex++;

    Debug.Log("Enemies Alive: " + enemiesAlive); // Print the number of enemies alive
}

void GenerateRandomPatterns(int totalEnemies)
{
    while (totalEnemies > 0)
    {
        // Randomly select an enemy prefab from the available prefabs
        GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Randomly select a pattern type
        string[] patternTypes = { "Horizontal", "Vertical", "Diagonal", "Spiral", "Circle", "Cross" };
        string randomPattern = patternTypes[Random.Range(0, patternTypes.Length)];

        // Randomly decide the number of enemies for this pattern (but not more than the remaining enemies)
        int enemiesForThisPattern = Random.Range(1, totalEnemies + 1);

        SpawnPattern(enemiesForThisPattern, randomEnemyPrefab, randomPattern);

        totalEnemies -= enemiesForThisPattern;
    }
}


void SpawnPattern(int count, GameObject enemyPrefab, string pattern)
{
    Vector2 spawnPosition = Vector2.zero;
    float offset = 1.5f; // Distance from the camera's edge

    for (int i = 0; i < count; i++)
    {
        switch (pattern)
        {
            case "Horizontal":
                spawnPosition = new Vector3(i - count / 2, GetSpawnOffset().y + offset, 0);
                break;
            case "Vertical":
                spawnPosition = new Vector3(GetSpawnOffset().x + offset, i - count / 2, 0);
                break;
            case "Diagonal":
                spawnPosition = new Vector3(i - count / 2, i - count / 2, 0) + new Vector3(GetSpawnOffset().x, GetSpawnOffset().y, 0) + offset * Vector3.one;
                break;
            case "Spiral":
                float angle = i * 25 * Mathf.Deg2Rad; // 25 degrees between each enemy
                spawnPosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * i * 0.5f; // 0.5f determines the distance between each enemy in the spiral
                break;
            case "Circle":
                float circleAngle = i * (360f / count) * Mathf.Deg2Rad;
                spawnPosition = new Vector3(Mathf.Cos(circleAngle), Mathf.Sin(circleAngle), 0) * 5f; // 5f is the radius of the circle
                break;
            case "Cross":
                if (i < count / 2)
                    spawnPosition = new Vector3(i - count / 4, 0, 0);
                else
                    spawnPosition = new Vector3(0, i - 3 * count / 4, 0);
                break;
            // ... Add more patterns as needed
        }

        Instantiate(enemyPrefab, spawnPosition + GetSpawnOffset(), Quaternion.identity);
        enemiesAlive++;
    }
}

    Vector2 GetSpawnOffset()
    {
        Vector2 topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        return topRight;
    }

    public void EnemyDied()
    {
        enemiesAlive--;
        Debug.Log("Enemies Alive: " + enemiesAlive); // Print the number of enemies alive
    }
}
