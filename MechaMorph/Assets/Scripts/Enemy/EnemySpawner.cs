using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] EnemyAi;
    private int arrayLength;
    public float spawnDelay = 2f;
    // Start is called before the first frame update
    void Start()
    {
        EnemySpawn();
        StartCoroutine(SpawnEnemiesDelay());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SpawnEnemiesDelay());
        EnemySpawn();
    }

    //Spawning Delay
    private IEnumerator SpawnEnemiesDelay()
    {
        while (true) 
        {
            EnemySpawn();
            yield return new WaitForSeconds(spawnDelay);
        }
    }
    //Enemy Spawn Function
    void EnemySpawn()
    {
        arrayLength = EnemyAi.Length;
        int randomValue = Random.Range(0, arrayLength);
        Instantiate(EnemyAi[randomValue], transform.position, transform.rotation);
    }
}
