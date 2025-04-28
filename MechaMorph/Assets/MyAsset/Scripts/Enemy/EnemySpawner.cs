using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
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
        private  bool isBossSpwaned;
        private int phaseLevel=0;
        private GameObject currentBoss;
        [SerializeField]private float negativeCounter = 300f;
        [SerializeField] private TextMeshProUGUI text;
        void Start()
        {
            StartCoroutine(SpawnLoop());
        }
        void Update()
        {
            timeCounter += Time.deltaTime;
            counter += Time.deltaTime;
            negativeCounter -= Time.deltaTime;
            if(!isBossSpwaned)  BossSpawnUI();
           
            if (timeCounter >= 30f && phaseLevel == 0) // 5-10 min
            {
                spawnDelay = 3f;
                phaseLevel = 1;
            }
            else if (timeCounter >= 600f && phaseLevel == 1) // 10-15 min
            {
                spawnDelay = 2f;
                phaseLevel = 2;
            }
            else if (timeCounter >= 900f && phaseLevel == 2) // after 15 min
            {
                spawnDelay = 0.5f;
                phaseLevel = 3;
            }

            if (!IsBossAvailable() && (counter >= spawnDelay || activeEnemies.Count == 0))
            {
                SpawnEnemy();
                counter = 0f;
            }

            if (phaseLevel >= 1 && !IsBossAvailable() && negativeCounter <= 0f)
            {
                BossSpawn();
            }
        }

        private void BossSpawnUI()
        {
            float difference = negativeCounter - counter;
            if (text!=null && difference > 0f)
            {
                text.text = $"Boss Appearing in : {(int)difference}s";
            }
        }
        private IEnumerator SpawnLoop()
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnEnemy();
        }
        private void BossSpawn()
        {
            Vector3 spawnPosition = new(
                Random.Range(xMinVal, xMaxVal),
                spawnHeight,
                Random.Range(zMinVal, zMaxVal)
            );
          currentBoss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
            activeEnemies.Add(currentBoss);
            counter = 0f;
            negativeCounter = 300f;
            isBossSpwaned = true;
            text.text = "";
        }
        public bool IsBossAvailable()
        {
            if (currentBoss == null)
            {
                isBossSpwaned = false;
                return false;
            }
            else
            {
                return true;
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
            counter = 0f;   
        }

        public void RemoveEnemy(GameObject enemy)
        {
            activeEnemies.Remove(enemy);

            // If the removed enemy is the boss
            if (enemy == currentBoss)
            {
                currentBoss = null;
                isBossSpwaned = false;
            }
        }
    }
}
