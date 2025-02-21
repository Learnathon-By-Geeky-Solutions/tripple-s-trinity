using System.Collections;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Enemy
{
    public class ExplosiveEnemyAi : EnemyAi
    {
        private bool hasExploded ;
        private bool hasStartedMoving ; // Ensure the enemy has moved at least once

        protected override void Update()
        {
            if (Agent == null) return; // Use 'Agent' instead of 'agent'
            base.Update();

            // Ensure the enemy has started moving at least once before checking AutoBreak
            if (!hasStartedMoving && Agent.velocity.magnitude > 0.1f)
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
            Explode(this); // 🔧 Pass the instance to the static method
        }

        bool AutoBreak()
        {
            if (!Agent.isOnNavMesh) return false; // Use 'Agent' instead of 'agent'

            // Ensure enemy is fully stopped before exploding
            if (Agent.remainingDistance <= 5f && Agent.velocity.magnitude < 0.1f)
            {
                if (!Agent.autoBraking)
                    Agent.autoBraking = true;

                return true;
            }
            return false;
        }

        public static void Explode(ExplosiveEnemyAi enemy)
        {
            if (enemy == null) return;
            Debug.Log("Explosion triggered!");
            Destroy(enemy.gameObject);
        }
    }
}