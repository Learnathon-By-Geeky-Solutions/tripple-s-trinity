using UnityEngine;
using UnityEngine.AI;

namespace TrippleTrinity.MechaMorph.Enemy
{
    public class Animationc : EnemyAi
    {
        private Animator animator;
        private NavMeshAgent agent1;
        [SerializeField] private Transform player;
        [SerializeField] private float stoppingDistance = 5f;
        private static readonly int KeepHittingHash = Animator.StringToHash("Keep Hitting");
        private static readonly int BlendTreeHash = Animator.StringToHash("Blend Tree");
        private new void Start()
        {
            agent1 = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player").transform;

            agent1.stoppingDistance = stoppingDistance;
            agent1.isStopped = false;
        }

        private new void Update()
        {

            if (player != null)
            {
                GameObject found = GameObject.FindGameObjectWithTag("Player");
                if (player != null && found != null)
                {
                    player = found.transform;
                }
                else
                {
                    return; // Player hasn't spawned yet
                }


                float distance = Vector3.Distance(player.position, transform.position);

                if (animator != null && agent1 != null)
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
        }

        public void HittingAnimation()
        {
            agent1.isStopped = false;
            agent1.SetDestination(player.position);

            animator.SetTrigger(KeepHittingHash);
            animator.ResetTrigger(BlendTreeHash);
        }
        public void MovingAnimation()
        {
            MoveTowardsTarget();
            agent1.isStopped = true;
            animator.SetTrigger(BlendTreeHash);
            animator.ResetTrigger(KeepHittingHash);
        }
        
    }
}