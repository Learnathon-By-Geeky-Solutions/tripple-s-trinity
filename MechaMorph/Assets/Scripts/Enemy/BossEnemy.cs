using System.Collections;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Enemy
{
    public class BossEnemy : EnemyAi
    {
        private readonly float xVal = 5f, yVal = 5f, zVal = 5f;
        private bool isTeleporting;

        protected override void Update()
        {
          
            if (!isTeleporting)
            {
                StartCoroutine(TeleportRoutine());
            }
        }

        void Disappear()
        {
            Vector3 destination = Boss_Destination();
            if (agent.isOnNavMesh)
            {
                agent.Warp(destination);
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

            if (Vector3.Distance(direction, transform.position) <= 5f)
            {
                return new Vector3(xPos, yPos, zPos);
            }

            return transform.position;
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