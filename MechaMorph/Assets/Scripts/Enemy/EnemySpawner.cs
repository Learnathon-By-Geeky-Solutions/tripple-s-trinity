
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TrippleTrinity.MechaMorph.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] enemyAi;
        [SerializeField] private GameObject bossPrefab;
        
        private readonly List<GameObject> activeEnemies = new();
        [SerializeField]private float spawnDelay = 6f;
        private float timeCounter;
        private float counter;
        
        private readonly float xMinVal = -13f, xMaxVal = 13f, zMinVal = -20f, zMaxVal = 20f;
        private readonly float spawnHeight = 1f;

        void Start()
        {
            StartCoroutine(SpawnLoop());
        }

        void Update()
        {
            timeCounter += Time.deltaTime;
            counter += Time.deltaTime;
            if (enemyAi.Length == 0)
            {
                SpawnEnemy();
            }
            // Update lap based on elapsed time
            if (timeCounter >= 900f) // 15+ minutes
            {
                
                spawnDelay = 0.5f;
            }
            else if (timeCounter >= 600f) // 10-15 minutes
            {
             
                spawnDelay = 2f;
            }
            else if (timeCounter >= 300f) // 5-10 minutes
            {
               
                spawnDelay = 3f;
            }
            
            // Spawn enemy if the delay time is met
            if (counter >= spawnDelay || activeEnemies.Count == 0)
            {
                 SpawnEnemy();
                counter = 0f; // Reset counter
               
            }
        }

        private IEnumerator SpawnLoop()
        {
         
                yield return new WaitForSeconds(spawnDelay);

                
                    SpawnEnemy();
                
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
            counter = 0f;   
        }

        public void RemoveEnemy(GameObject enemy)
        {
            activeEnemies.Remove(enemy);
        }
    }
}
