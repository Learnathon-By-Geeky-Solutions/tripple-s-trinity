using System.Collections;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Enemy
{
    public class EnemyWeapon : MonoBehaviour
    {
        private bool isShooting;
        private bool detected;
        [SerializeField] private float detectionRadius = 20f;
        [SerializeField] private float projectileSpeed = 10f;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private GameObject projectilePrefab;

        void Update()
        {
            if (detected && !isShooting)
            {
                StartCoroutine(WaitToShoot());
            }
            else if (!detected)
            {
                Debug.Log("Player is out of range.");
            }

            EnemyShooting();
        }

        void EnemyShooting()
        {
            Vector3 origin = new Vector3(0,0,transform.position.z);
            Vector3 direction = transform.forward;
            
            
            RaycastHit hit;

           
            if (Physics.Raycast(origin, direction, out hit, detectionRadius))

            {
                if (hit.collider.CompareTag("Player"))
                {
                    detected = true;

                    // Rotate toward the player
                    FacePlayer(hit.collider.transform);
                }
            }
            else
            {
                detected = false;
            }
        }

        void FacePlayer(Transform playerTransform)
        {
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        void Shoot()
        {
            if (shootPoint != null && projectilePrefab != null)
            {
                GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
                Rigidbody rb = projectile.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.velocity = transform.forward * projectileSpeed;
                }
            }
        }

        IEnumerator WaitToShoot()
        {
            isShooting = true;
            Shoot();
            yield return new WaitForSeconds(1f); // Delay between shots
            isShooting = false;
        }
    }
}
