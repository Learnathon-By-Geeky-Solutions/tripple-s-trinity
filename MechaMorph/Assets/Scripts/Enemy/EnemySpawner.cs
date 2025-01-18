using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TrippleTrinity.MechaMorph.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] enemyAi;
        private int arrayLength;
        [SerializeField] private float spawnDelay = 10f;
        private readonly float xMinVal = -13f, xMaxVal = 13f, zMinVal = -20f, zMaxVal = 20f;
        private float xVal, zVal;
        private readonly float spawnHeight = 5f;
        [SerializeField] private float countDown = 10f;
        private List<GameObject> gameObjects;
        void Start()
        {
            arrayLength = enemyAi.Length;
            gameObjects = new List<GameObject>();
            StartCoroutine(SpawnEnemiesDelay());
          
        }

        void Update()
        {
            if (countDown > 0)
            {
                   countDown -= Time.deltaTime;
            }
            else if (countDown<=0 && gameObjects.Count > 0)
            {
                ClearGameObjects();
                Debug.Log("All game objects are cleared");
            }
        }
        private IEnumerator SpawnEnemiesDelay()
        {
            while (countDown>=0f)
            {
                xVal = Random.Range(xMinVal, xMaxVal);
                zVal = Random.Range(zMinVal, zMaxVal);
                EnemySpawn();
                yield return new WaitForSeconds(spawnDelay);
            }
        }

        private void EnemySpawn()
        {
            if(enemyAi==null || enemyAi.Length ==0)
            {
                Debug.LogError("The Array is empty!");
                return;
            }
            int randomValue = Random.Range(0, arrayLength);
            Debug.Log($"Spawning Enemy at Index: {randomValue}");
            Vector3 spawnPosition = new Vector3(xVal, spawnHeight, zVal);
           GameObject prefab= Instantiate(enemyAi[randomValue], spawnPosition, Quaternion.identity);
            gameObjects.Add(prefab);
            Debug.Log($"GameObjects Count: {gameObjects.Count}");
        }

        private void ClearGameObjects()
        {
            foreach (var obj in gameObjects)
            {
                Destroy(obj);
            }
            gameObjects.Clear();
        }
    }
}
