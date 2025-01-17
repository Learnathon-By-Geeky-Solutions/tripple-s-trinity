using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


namespace TrippleTrinity.MechaMorph.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] EnemyAi;
        private int arrayLength;
        [SerializeField] private float spawnDelay = 10f;
         private float xMinVal = -13f, xMaxVal=-13f, zMinVal = -20f, zMaxVal = 20f;
        private float xVal,zVal;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(SpawnEnemiesDelay());

        }
     
        //Spawning Delay
        private IEnumerator SpawnEnemiesDelay()
        {
            while (true)
            {
                RandomPlaceToSpawn();
                EnemySpawn();
                yield return new WaitForSeconds(spawnDelay);
            }
        }
        //Enemy Spawn Function
        void EnemySpawn()
        {
            arrayLength = EnemyAi.Length;
            int randomValue = Random.Range(0, 2);
            Debug.Log(randomValue);
            Vector3 spawnPosition = new Vector3(xVal, 0, zVal);
            Instantiate(EnemyAi[randomValue], spawnPosition, Quaternion.identity);
        }
        void RandomPlaceToSpawn()
        {
          xVal = Random.Range(xMinVal,xMaxVal);
          zVal= Random.Range(zMinVal,zMaxVal);
         
        }
    }
}