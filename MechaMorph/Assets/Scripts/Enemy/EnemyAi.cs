
using UnityEngine;
using UnityEngine.AI;

namespace TrippleTrinity.MechaMorph.Enemy
{
    public class EnemyAi : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        public NavMeshAgent agent;
        [SerializeField] private int health;
        private Transform targetPosition;
        [SerializeField] private GameObject bullet;
        [SerializeField] private float moveRotationSpeed = 45f;

        // 🔧 Declare direction as a private field
        protected Vector3 Direction; 
        
        protected virtual void Start()
        {
            agent = GetComponent<NavMeshAgent>();

            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                targetPosition = playerObject.transform;
                Debug.Log($"Player found: {targetPosition.name}");
            }
            else
            {
                Debug.Log("Player not found! Make sure the Player object is tagged correctly.");
            }

            agent.autoBraking = false;
        }

        protected virtual void Update()
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                targetPosition = playerObject.transform;
            }

            if (targetPosition != null)
            {
                RotateTowardsTarget();
                MoveTowardsTarget();
            }
        }

        protected virtual void MoveTowardsTarget()
        {
            if (targetPosition == null) return;
            if (agent!=null && agent.isOnNavMesh)
            {
                agent.SetDestination(targetPosition.position);
            }
        }

        protected virtual void RotateTowardsTarget()
        {
            if (targetPosition == null) return;

            // 🔧 Now 'direction' is correctly assigned
            Direction = (targetPosition.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(Direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                Time.deltaTime * moveRotationSpeed
            );
        }

        protected virtual void Die()
        {
            Debug.Log("Enemy is dead.");
            Destroy(gameObject);
        }
    }
}
