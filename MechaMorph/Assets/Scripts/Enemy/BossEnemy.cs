using UnityEngine; // Use UnityEngine for Transform and Vector3

namespace TrippleTrinity.MechaMorph.Enemy
{
    public class BossEnemy : EnemyAi
    {
        private Transform bossTransform;
        private readonly float xVal = 5f, yVal = 5f, zVal = 5f;
        private bool hasWarped = false;
        protected override void Update()
        {
            if (!hasWarped)
            {
                Disappear();
                hasWarped = true;
            }
        }

        void Disappear()
        {
           
            Vector3 destination = Boss_Destination();
            if (agent.isOnNavMesh) 
            {
                agent.Warp(destination);
                hasWarped = false;
            }
            else
            {
                Debug.LogWarning("Agent is not on a NavMesh. Cannot warp.");
            }
        }

        Vector3 Boss_Destination()
        {
            
            if (Vector3.Distance(direction, transform.position) <= 5f)
            {
              
                return new Vector3(xVal, yVal, zVal);
            }

          
            return transform.position;
        }
    }
}