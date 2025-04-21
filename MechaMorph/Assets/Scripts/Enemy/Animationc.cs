using UnityEngine;
using UnityEngine.AI;

namespace TrippleTrinity.MechaMorph.Enemy
{
    public class Animationc : EnemyAi
    {
        private Animator animator;
        private NavMeshAgent agent;
        [SerializeField] private Transform player;
        [SerializeField] private float stoppingDistance = 5f;
        private static readonly int KeepHittingHash = Animator.StringToHash("Keep Hitting");
        private static readonly int BlendTreeHash = Animator.StringToHash("Blend Tree");
        private new void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player").transform;

            agent.stoppingDistance = stoppingDistance;
            agent.isStopped = false;
        }

        private new void Update()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            if (player == null)
            {
                GameObject found = GameObject.FindGameObjectWithTag("Player");
                if (found != null)
                {
                    player = found.transform;
                }
                else
                {
                    return; // Player hasn't spawned yet
                }
            }

            float distance = Vector3.Distance(player.position, transform.position);

            if (animator != null && agent != null)
            {
                if (distance <= stoppingDistance)
                {
                    HittingAnimation();
                }
                else
                {
                    MovingAnimation();
                }
            }
        }

        public void HittingAnimation()
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);

            animator.SetTrigger(KeepHittingHash);
            animator.ResetTrigger(BlendTreeHash);
        }
        public void MovingAnimation()
        {
            MoveTowardsTarget();
            agent.isStopped = true;
            animator.SetTrigger(BlendTreeHash);
            animator.ResetTrigger(KeepHittingHash);
        }
        
    }
}