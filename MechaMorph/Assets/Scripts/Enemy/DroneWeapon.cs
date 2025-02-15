using UnityEngine;
using UnityEngine.AI;
using System.Collections;
namespace TrippleTrinity.MechaMorph.Enemy
{
    public class DroneWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private float fireRate = 1f;
        [SerializeField] private float bulletSpeed = 15f;
        private float nextFireTime = 0f;

        private Transform player;
        private Rigidbody playerRb;
        private NavMeshAgent agent; // NavMeshAgent reference

        void Start()
        {
            agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component

            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
                playerRb = playerObject.GetComponent<Rigidbody>(); // Get player's velocity if available
            }
            else
            {
                Debug.LogWarning("Player not found! Make sure the Player object is tagged correctly.");
            }
        }

        void Update()
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
                playerRb = playerObject.GetComponent<Rigidbody>(); // Get player's velocity if available
            }
            else
            {
                Debug.LogWarning("Player not found! Make sure the Player object is tagged correctly.");
            }
            if (player == null || agent == null) return;

            // Check if the agent is stopped or auto-breaking
            if (IsAgentStoppedOrAutoBraking())
            {
                // Predict future position
                Vector3 predictedPosition = PredictPlayerPosition();

                // Rotate drone instantly towards predicted position for better accuracy
                transform.LookAt(predictedPosition);

                if (Time.time >= nextFireTime)
                {
                    Shoot(predictedPosition);
                    nextFireTime = Time.time + fireRate;
                }
            }
        }

        private bool IsAgentStoppedOrAutoBraking()
        {
            return agent.velocity.magnitude < 0.1f || agent.isStopped || agent.autoBraking;
        }

        private Vector3 PredictPlayerPosition()
        {
            if (playerRb != null)
            {
                float timeToTarget = Vector3.Distance(transform.position, player.position) / bulletSpeed;
                return player.position + playerRb.velocity * timeToTarget;
            }
            return player.position; // Fallback to direct aiming
        }

        private void Shoot(Vector3 targetPosition)
        {
            if (bulletPrefab == null || bulletSpawnPoint == null)
            {
                Debug.LogWarning("BulletPrefab or BulletSpawnPoint is missing!");
                return;
            }

            // Ensure the bullet is facing the correct direction
            Quaternion bulletRotation = Quaternion.LookRotation(targetPosition - bulletSpawnPoint.position);

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletRotation);
            
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 shootDirection = (targetPosition - bulletSpawnPoint.position).normalized;
                rb.velocity = shootDirection * bulletSpeed;
            }
            DestroyPooledObject(bullet);
        }
        public void DestroyPooledObject(GameObject bullet)
        {
            StartCoroutine(DeactivateAfterTime(bullet, 6f));
        }

        private IEnumerator DeactivateAfterTime(GameObject obj, float delay)
        {
            yield return new WaitForSeconds(delay);
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
}
