using UnityEngine;

namespace TheDeveloperTrain.SciFiGuns
{
    public class GunSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] guns;
        private GameObject[] currentGuns = new GameObject[4];
        private int spawnIndex = 0;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            currentGuns[0] = Instantiate(guns[0]);
            currentGuns[1] = Instantiate(guns[1], Vector3.right * 5, Quaternion.identity);
            currentGuns[2] = Instantiate(guns[2], Vector3.right * 10, Quaternion.identity);
            currentGuns[3] = Instantiate(guns[3], Vector3.right * 15, Quaternion.identity);
            spawnIndex += 4;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                for (int i = 0; i < 4; i++)
                {
                    Destroy(currentGuns[i]);
                    currentGuns[i] = Instantiate(guns[spawnIndex + i], 5 * i * Vector3.right, Quaternion.identity);
                }
                spawnIndex += 4;
                spawnIndex %= guns.Length;
            }
        }
    }
}

