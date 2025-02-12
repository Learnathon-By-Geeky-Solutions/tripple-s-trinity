using System.Collections;

using UnityEngine; // Use UnityEngine for Transform and Vector3

namespace TrippleTrinity.MechaMorph.Enemy
{
    public class BossEnemy : EnemyAi
    {
        private Transform bossTransform;
        private float xVal, yVal, zVal;
        private readonly float xPos=13f,zPos=20f,yPos=5f ;
        private bool hasWarped;
        protected override void Update()
        {
          
            //Boss Teleportation
            if (!hasWarped)
            {
                StartCoroutine(Timer());
                hasWarped = true;
            }
            
            //Boss Flying Ability upon some conditions
            
        }
      
        //Boss Teleportation
        void Disappear()
        {
           
            Vector3 destination = Boss_Destination();
            if (agent.isOnNavMesh) 
            {
                agent.Warp(destination);
                Vector3 desiredVelocity = agent.desiredVelocity;
                agent.velocity = desiredVelocity;
                hasWarped = false;
            }
            else
            {
                Debug.LogWarning("Agent is not on a NavMesh. Cannot warp.");
            }
        }
        
        //Teleport Destination
        Vector3 Boss_Destination()
        {
             xVal = Random.Range(-xPos, xPos );
             yVal = Random.Range(-yPos, yPos );
             zVal = Random.Range(-zPos, zPos );
            return new Vector3(xVal, yVal, zVal);
        }
      
        //Teleport Timer
        IEnumerator Timer()
        {
            hasWarped = true;
            yield return new WaitForSeconds(5f);
            MoveTowardsTarget();
            Boss_Destination();
            Disappear();
        }
    }
}