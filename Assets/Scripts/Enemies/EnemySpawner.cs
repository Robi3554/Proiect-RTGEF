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
            foreach(var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                if(enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    if(enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }

                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPositions[Random.Range(0, relativeSpawnPositions.Count)].position, Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }

        if(enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }

    public void OnEnemyKilled()
    {
        enemiesAlive--;
    }

    private IEnumerator BeginNextWave()
    {
        isTransitioningWave = true;

        yield return new WaitForSeconds(waveInterval);

        if (currentWaveCount < waves.Count - 1 &&
            waves[currentWaveCount].spawnCount >= waves[currentWaveCount].waveQuota &&
            enemiesAlive == 0)
        {
            currentWaveCount++;

            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                enemyGroup.spawnCount = 0;
            }
            waves[currentWaveCount].spawnCount = 0;

            CalculateWaveQuota();
            GameManager.Instance.IncreaseMultiplier(0.1f);

        }

        isTransitioningWave = false;
    }
}
