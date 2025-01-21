
using UnityEngine;


namespace TrippleTrinity.MechaMorph.Enemy
{
    public class ExplosiveEnemyAi : EnemyAi
    {

        private bool hasExploded;
        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            if (!hasExploded && AutoBreak())
            {
                hasExploded = true;
                Explode();
            }
        }
        bool AutoBreak()
        {
            if (agent.isOnNavMesh && agent.remainingDistance <= 5f && agent.velocity.magnitude > 0.1f)
            {
                agent.autoBraking = true;
                return true;
            }
            return false;
        }
        void Explode()
        {
            //ExplosionSound();
            Debug.Log("Death");
            Destroy(this.gameObject);
        }

    }
}
