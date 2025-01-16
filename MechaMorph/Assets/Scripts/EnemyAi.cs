

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
//com.company.subject
//com.TrippleTrinity.MechaMorph.Ai
namespace TrippleTrinity.MechaMorph.Enemy
{
    public class EnemyAi : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private Rigidbody rb;
        protected NavMeshAgent agent;
        [SerializeField] private int health;
        [SerializeField] private Transform targetPosition;
        [SerializeField] private GameObject Bullet => _bullet;
        [SerializeField] GameObject _bullet;
        [SerializeField] private float MoveRotationSpeed = 45f;
        [SerializeField] private Vector3 direction;


        // Start is called before the first frame update
        protected virtual void Start()
        {
            //player = TransformManager.currentForm;
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Debug.Log($"player.name)");
            }
            agent = GetComponent<NavMeshAgent>();
            agent.autoBraking = false;

        }

        // Update is called once per frame
        protected virtual void Update()
        {

            if (targetPosition != null)
            {
                RotateTowardsTarget();
                //EnemyAI will move towards the enemy
                MoveTowardsTarget();
            }
        }

        //Move towards the player
        protected virtual void MoveTowardsTarget()
        {
            agent.SetDestination(player.transform.position);
        }

        //Rotate towards the Player
        protected virtual void RotateTowardsTarget()
        {
            direction = (player.transform.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                Time.deltaTime * MoveRotationSpeed
                );
        }
        //EnemyAi Death
        protected virtual void Die()
        {
            Debug.Log("Is dead");
            Destroy(gameObject);
        }
    }

}
