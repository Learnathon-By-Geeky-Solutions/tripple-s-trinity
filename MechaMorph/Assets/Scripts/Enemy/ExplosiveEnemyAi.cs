using System.Collections;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Enemy
{
    public class ExplosiveEnemyAi : EnemyAi
    {
        private bool hasExploded = false;
        private bool hasStartedMoving = false; // Ensure the enemy has moved at least once
        
        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            if (agent == null) return;
            base.Update();

            // Ensure the enemy has started moving at least once before checking AutoBreak
            if (!hasStartedMoving && agent.velocity.magnitude > 0.1f)
            {
                hasStartedMoving = true;
            }

            if (hasStartedMoving && !hasExploded && AutoBreak())
            {
                hasExploded = true; // Mark explosion to prevent multiple triggers
                StartCoroutine(ExplodeWait());
            }
            
            
        }

        IEnumerator ExplodeWait()
        {
            yield return new WaitForSeconds(0.5f);
            Explode();
        }

        bool AutoBreak()
        {
            if (!agent.isOnNavMesh) return false;

            // Ensure enemy is fully stopped before exploding
            if (agent.remainingDistance <= 5f && agent.velocity.magnitude < 0.1f)
            {
                if (!agent.autoBraking)
                    agent.autoBraking = true;

                return true;
            }
            return false;
        }

        void Explode()
        {
            
            Debug.Log("Explosion triggered!");
            Destroy(gameObject);
        }
    }
}