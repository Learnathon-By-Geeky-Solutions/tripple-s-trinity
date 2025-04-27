using UnityEngine;

namespace TrippleTrinity.MechaMorph.Enemy
{
    public class ExplosiveEnemyAnimation : EnemyAi
    {
         
        private Animator animator;
        [SerializeField] private float stoppingDistance = 5f;
        [SerializeField] private GameObject explosionEffect;
        private static readonly int IdleHash = Animator.StringToHash("Idle");
        private static readonly int WalkHash = Animator.StringToHash("walk");
        [SerializeField] private AudioClip audioClip;
        protected override void Start()
        {
            base.Start(); // Call EnemyAi's Start()

            animator = GetComponent<Animator>();

            if (Agent != null)
            {
                Agent.stoppingDistance = stoppingDistance;
                Agent.isStopped = false;
            }
        }

        protected override void Update()
        {
            base.Update(); // Calls RotateTowardsTarget() etc.

            if (targetPosition == null || Agent == null || animator == null) return;

            float distance = Vector3.Distance(transform.position, targetPosition.position);

            if (distance <= stoppingDistance)
            {
                TriggerIdle(); 
                Explode();// Within range
            }
            else
            {
                TriggerWalk(); // Outside range
            }
        }

        private void TriggerIdle()
        {
            Agent.isStopped = true;

            animator.ResetTrigger(WalkHash);
            animator.SetTrigger(IdleHash);
        }

        private void TriggerWalk()
        {
            Agent.isStopped = false;
            Agent.SetDestination(targetPosition.position);

            animator.ResetTrigger(IdleHash);
            animator.SetTrigger(WalkHash);
        }
        void Explode()
        {
            if(explosionEffect!=null)
            { 
                AudioSource.PlayClipAtPoint(audioClip, transform.position,1f);
                //Particle Effect
                Instantiate(explosionEffect,transform.position,transform.rotation);
                
            }
           
            Destroy(gameObject);
        }
    }
}