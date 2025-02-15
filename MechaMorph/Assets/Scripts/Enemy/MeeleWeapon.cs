using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace TrippleTrinity.MechaMorph.Enemy
{
    public class MeeleWeapon : MonoBehaviour
    {
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private float fireRate = 1f;
        [SerializeField] private float bulletSpeed = 15f;
        private float nextFireTime = 0f;
        private Transform player;
        private Rigidbody playerRb;
        private NavMeshAgent agent;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();

            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
                playerRb = playerObject.GetComponent<Rigidbody>();
            }
            else
            {
                Debug.LogWarning("Player not found! Make sure the Player object is tagged correctly.");
            }
        }

        void Update()
        {
            if (player == null || agent == null) return;

            if (IsAgentStoppedOrAutoBraking())
            {
                Vector3 predictedPosition = PredictPlayerPosition();
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
            return player.position;
        }

        private void Shoot(Vector3 targetPosition)
        {
            if (bulletSpawnPoint == null)
            {
                Debug.LogWarning("BulletSpawnPoint is missing!");
                return;
            }

            GameObject bullet = ObjectPool.instance.GetPooledObject();
            if (bullet != null)
            {
                bullet.transform.position = bulletSpawnPoint.position;
                bullet.transform.rotation = Quaternion.LookRotation(targetPosition - bulletSpawnPoint.position);
                bullet.SetActive(true);

                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = Vector3.zero; // Reset velocity before applying new one
                    rb.angularVelocity = Vector3.zero; // Reset rotation movement
                    Vector3 shootDirection = (targetPosition - bulletSpawnPoint.position).normalized;
                    rb.velocity = shootDirection * bulletSpeed;
                }

                // Call destroy method to deactivate bullet after 6 seconds
                DestroyPooledObject(bullet);
            }
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
