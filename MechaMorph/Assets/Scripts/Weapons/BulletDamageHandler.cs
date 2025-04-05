using UnityEngine;
using TrippleTrinity.MechaMorph.Damage;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class BulletDamageHandler : MonoBehaviour
    {
        private float _damage;
        private int _targetLayer;

        public void Initialize(float damage, string shooterTag)
        {
            _damage = damage;
            _targetLayer = LayerMask.NameToLayer(shooterTag == "Enemy" ? "Player" : "Enemy");
        }

        public void HandleCollision(Collider other)
        {
            if (other.gameObject.layer != _targetLayer) return; // Ignore unintended targets

            Damageable targetHealth = other.GetComponentInParent<Damageable>();
            targetHealth?.TakeDamage(_damage);

            Debug.Log($"Bullet hit {other.gameObject.name}! Dealt {_damage} damage.");
        }
    }
}