
using UnityEngine;
using TrippleTrinity.MechaMorph.Health;  
using TrippleTrinity.MechaMorph.Damage;
namespace TrippleTrinity.MechaMorph.Enemy
{
    public class GrenadeSpawner : MonoBehaviour
    {
        private int _damage = 5;
        //grenade force
        [SerializeField] private float _force = 100f;
        [SerializeField]private float _radius = 2f;
        private void Start()
        {
            //collision with particle effect
            Explode();
            Destroy(this.gameObject,3f);
        }

        void Explode()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);
            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(_force, transform.position, _radius);
                }
            }
        }

        void OnParticleCollision(GameObject other)
        {
            Debug.Log($"Particle hit: {other.name}"); // Debugging

            if (other.CompareTag("Player")) 
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>() ?? other.GetComponentInParent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(_damage);
                    Debug.Log($"Enemy grenade hit Player! Dealt {_damage} damage.");
                }
                else
                {
                    Debug.LogWarning("PlayerHealth not found on Player!");
                }
            }
        }

    }
}