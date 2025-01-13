using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups;
        public int waveQuota;
        public float spawnInterval;
        public float spawnCount;
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;
        public int spawnCount;
        public GameObject enemyPrefab;
    }

    private Transform player;

    public List<Wave> waves;
    public int currentWaveCount;

    [Header("Spawner Attributes")]
    float spawnTimer;
    public float waveInterval;
    public int enemiesAlive;
    public int maxEnemiesAllowed;
    public bool maxEnemiesReached = false;

    private bool isTransitioningWave;

    public TMP_Text waveText;

    [Header("Spawn Positions")]
    public List<Transform> relativeSpawnPositions;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        CalculateWaveQuota();
    }

    void Update()
    {
        if (!isTransitioningWave &&  
            currentWaveCount < waves.Count &&
            waves[currentWaveCount].spawnCount >= waves[currentWaveCount].waveQuota &&
            enemiesAlive == 0)
        {
            StartCoroutine(BeginNextWave());
        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= waves[currentWaveCount].spawnInterval &&
            waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }

        waveText.text = "Wave : " + (currentWaveCount + 1).ToString();
    }

    private void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;

        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
    }

    private void SpawnEnemies()
    {
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    if (enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }

                    var spawnPosition = player.position + relativeSpawnPositions[Random.Range(0, relativeSpawnPositions.Count)].position;
                    Instantiate(enemyGroup.enemyPrefab, spawnPosition, Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }

        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }


    public void OnEnemyKilled()
    {
        if (enemiesAlive > 0)
        {
            enemiesAlive--;
            Debug.Log($"Enemy killed. Enemies alive: {enemiesAlive}");
        }
        else
        {
            Debug.LogWarning("Tried to decrease enemiesAlive when it is already 0 or negative.");
        }
    }

    private void ResetAllSpawnCounts()
    {
        foreach (var wave in waves)
        {
            wave.spawnCount = 0;
            foreach (var enemyGroup in wave.enemyGroups)
            {
                enemyGroup.spawnCount = 0;
            }
        }

        Debug.Log("All spawn counts reset as waves start over.");
    }

    private IEnumerator BeginNextWave()
    {
        isTransitioningWave = true;

        yield return new WaitForSeconds(waveInterval);

        if (waves[currentWaveCount].spawnCount >= waves[currentWaveCount].waveQuota &&
            enemiesAlive == 0)
        {
            currentWaveCount++;

            if (currentWaveCount >= waves.Count)
            {
                currentWaveCount = 0;
                GameManager.Instance.IncreaseMultiplier(0.5f);
                ResetAllSpawnCounts();
            }
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                enemyGroup.spawnCount = 0;
            }
            waves[currentWaveCount].spawnCount = 0;

            CalculateWaveQuota();
        }

        Debug.Log($"Transitioning to wave {currentWaveCount + 1}. Enemies alive: {enemiesAlive}");
        isTransitioningWave = false;
    }
}
