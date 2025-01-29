using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] enemyAi;
        [SerializeField] private GameObject bossPrefab;

        private readonly List<GameObject> activeEnemies = new(); 
        private int lap, waveIndex; 
        private bool bossSpawned;

        private readonly int[] waveEnemyCounts = { 5, 15, 20, 30 };
        private readonly float[] waveDelays = {0, 10f, 15f, 20f, 30f };

        private readonly float xMinVal = -13f, xMaxVal = 13f, zMinVal = -20f, zMaxVal = 20f;
        private readonly float spawnHeight = 1f;

        void Start()
        {
            // SpawnBoss();
            StartCoroutine(StartWaveSequence());
        }

        void Update()
        {
            if (!bossSpawned && activeEnemies.Count == 0 && waveIndex >= waveEnemyCounts.Length)
            {
                SpawnBoss();
            }
            else if (bossSpawned && activeEnemies.Count == 0)
            {
                StartNewLap();
            }
        }

        private IEnumerator StartWaveSequence()
        {
            while (!bossSpawned && waveIndex < waveEnemyCounts.Length)
            {
                yield return new WaitForSeconds(waveDelays[waveIndex]);
                SpawnWave(waveEnemyCounts[waveIndex]);
                waveIndex++;
            }
        }

        private void SpawnWave(int numOfEnemies)
        {
            if (enemyAi.Length == 0)
            {
                Debug.LogError("Enemy array is empty!");
                return;
            }

            Debug.Log($"Wave {waveIndex + 1}: Spawning {numOfEnemies} enemies.");
            for (int i = 0; i < numOfEnemies; i++)
            {
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
           
            Vector3 spawnPosition = new(
                Random.Range(xMinVal, xMaxVal),
                spawnHeight,
                Random.Range(zMinVal, zMaxVal)
            );

            GameObject enemy = Instantiate(enemyAi[Random.Range(0, enemyAi.Length)], spawnPosition, Quaternion.identity);
           
            activeEnemies.Add(enemy);
       
            
        }

        private void SpawnBoss()
        {
            if (!bossPrefab)
            {
                Debug.LogError("Boss prefab is not assigned!");
                return;
            }

            Debug.Log("Spawning Boss...");
            activeEnemies.Add(Instantiate(bossPrefab, Vector3.up * spawnHeight, Quaternion.identity));
            bossSpawned = true;
        }

        private void StartNewLap()
        {
            Debug.Log($"Lap {++lap} completed! Starting Lap {lap}...");
            waveIndex = 0;
            bossSpawned = false;
            StartCoroutine(StartWaveSequence());
        }
    }
}
