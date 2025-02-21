using UnityEngine;
using TrippleTrinity.MechaMorph.Weapons;

namespace TrippleTrinity.MechaMorph.Enemy
{
    public class RangedEnemyAi : EnemyAi
    {
        [SerializeField] private float attackRange = 7f;
        [SerializeField] private float fireRate = 1f;
        private float nextFireTime;

        private GunAbility gunAbility;

        protected override void Start()
        {
            base.Start();
            gunAbility = GetComponent<GunAbility>(); // Get the GunAbility component
            if (gunAbility == null)
            {
                Debug.LogError("GunAbility component is missing on RangedEnemyAi!");
            }
        }

        protected override void Update()
        {
            base.Update();

            if (targetPosition == null || gunAbility == null) return;

            float distanceToPlayer = Vector3.Distance(transform.position, targetPosition.position);

            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
            else
            {
                MoveTowardsTarget(); // Move closer if out of range
            }
        }

        private void AttackPlayer()
        {
            RotateTowardsTarget();

            if (Time.time >= nextFireTime)
            {
                Debug.Log("Enemy is shooting!"); // Debugging
                gunAbility.AITryShoot(); // ✅ Enemy fires ONLY using AI logic
                nextFireTime = Time.time + fireRate;
            }
        }

    }
}