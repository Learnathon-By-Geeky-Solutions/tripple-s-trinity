using System.Collections;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Enemy
{
    public class BossEnemy : EnemyAi
    {
        private readonly float xVal = 15f, yVal = 0f, zVal = 22f;
        private bool isTeleporting;

        protected override void Update()
        {
          base.Update();
            if (!isTeleporting)
            {
                StartCoroutine(TeleportRoutine());
            }
        }

        void Disappear()
        {
            Vector3 destination = Boss_Destination();
            if (Agent.isOnNavMesh)
            {
                Agent.Warp(destination);
                isTeleporting = false;
            }
            else
            {
                Debug.LogWarning("Agent is not on a NavMesh. Cannot warp.");
            }
        }

        Vector3 Boss_Destination()
        {
            float xPos = Random.Range(-xVal, xVal);
            float yPos = Random.Range(-yVal, yVal);
            float zPos = Random.Range(-zVal, zVal);

            Vector3 randomOffset = new Vector3(xPos, yPos, zPos);
            return transform.position+randomOffset;
        }

        IEnumerator TeleportRoutine()
        {
            isTeleporting = true;

            // Wait for 1 second before teleporting
            yield return new WaitForSeconds(5f);
           
            // Perform teleportation
            Disappear();
        }
    }
}