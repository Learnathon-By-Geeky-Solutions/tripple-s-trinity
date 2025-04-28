using TrippleTrinity.MechaMorph.MyAsset.Scripts.Weapons;
using TrippleTrinity.MechaMorph.SoundManager_main.SoundManager_main;
using UnityEngine;
using TrippleTrinity.MechaMorph.Weapons;

namespace TrippleTrinity.MechaMorph.Enemy
{
    public class RangedEnemyAi : EnemyAi
    {
        [SerializeField] private float attackRange = 7f;
        [SerializeField] private float fireRate = 1f;
        private float _nextFireTime;
        private GunAbility _gunAbility;
        protected override void Start()
        {
            base.Start();
            _gunAbility = GetComponent<GunAbility>(); // Get the GunAbility component
            if (_gunAbility == null)
            {
                Debug.LogError("GunAbility component is missing on RangedEnemyAi!");
            }
        }

        protected override void Update()
        {
            base.Update();

            if (targetPosition == null || _gunAbility == null) return;

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

            if (Time.time >= _nextFireTime)
            {
                Debug.Log("Enemy is shooting!"); // Debugging

                _gunAbility.TriggerShoot(); // Call the public TriggerShoot method
                _nextFireTime = Time.time + fireRate;
            }
        }
    }
}