
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Enemy
{
    public class ExplosiveEnemyAi : EnemyAi
    {
        private bool hasExploded ;
        private bool hasStartedMoving ; 
    

        [SerializeField] private float radius = 5f;
      
        [SerializeField] private GameObject explosionEffect;
        [SerializeField] private float force = 700f;
        
        protected override void Update()
        {
            if (Agent == null) return; 
            base.Update();
        
            if (!hasStartedMoving && Agent.velocity.magnitude > 0.1f)
            {
                hasStartedMoving = true;
            }

            if (hasStartedMoving && !hasExploded && AutoBreak())
            {
                    Explode(this);
                    hasExploded = true;
            }

        }
        
        bool AutoBreak()
        {
            if (!Agent.isOnNavMesh) return false; 

            
            if (Agent.remainingDistance <= 5f && Agent.velocity.magnitude < 0.1f)
            {
                if (!Agent.autoBraking)
                    Agent.autoBraking = true;

                return true;
            }
            return false;
        }

        void Explode(ExplosiveEnemyAi enemy)
        {
            if(explosionEffect!=null)
            { 
                //Particle Effect
                Instantiate(explosionEffect,transform.position,transform.rotation);
            }
           
            Destroy(enemy.gameObject);
        }
    }
    
}