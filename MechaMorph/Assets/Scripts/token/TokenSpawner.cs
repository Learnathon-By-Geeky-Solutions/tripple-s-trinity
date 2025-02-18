using UnityEngine;

namespace TrippleTrinity.MechaMorph.Token
{
    public class TokenSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] tokenPrefabs; // Assign in Inspector
        [SerializeField] private float dropChance = 0.5f; // 50% chance to drop a token

        private void Awake()
        {
            Debug.Assert(tokenPrefabs.Length > 0, "TokenSpawner: No token prefabs assigned!");
        }

        public void SpawnToken(Vector3 position)
        {
            if (Random.value > dropChance) return; // Random chance check

            int randomIndex = Random.Range(0, tokenPrefabs.Length);

            //  Spawn token at the given X and Z but force Y to -4
            Vector3 spawnPosition = new Vector3(position.x, -4f, position.z);
            
            Instantiate(tokenPrefabs[randomIndex], spawnPosition, Quaternion.identity);
        }
    }
}