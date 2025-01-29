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
        public  Vector3 direction;

        // Start is called before the first frame update
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

        // Update is called once per frame
        protected virtual void Update()
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                targetPosition = playerObject.transform;
                Debug.Log($"Player found: {targetPosition.name}");
            }

            if (targetPosition != null)
            {
                RotateTowardsTarget();
                MoveTowardsTarget();
            }
        }

        // Move towards the target
        protected virtual void MoveTowardsTarget()
        {
            if (targetPosition == null) return;
            if (agent.isOnNavMesh)
            {
                agent.SetDestination(targetPosition.position);
            }
        }

        // Rotate towards the target
        protected virtual void RotateTowardsTarget()
        {
            if (targetPosition == null) return;

            direction = (targetPosition.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                Time.deltaTime * moveRotationSpeed
            );
        }

        // Enemy death logic
        protected virtual void Die()
        {
            Debug.Log("Enemy is dead.");
            Destroy(gameObject);
        }
    }
}
