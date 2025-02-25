using UnityEngine;
using TrippleTrinity.MechaMorph.Health;  
namespace TrippleTrinity.MechaMorph.Enemy
{
    public class MeleeEnemyAi : MonoBehaviour
    {
        private int _damage= 1;
         void OnParticleCollision(GameObject other)
        {
            Debug.Log($"Particle hit: {other.name}"); 

            if (other.CompareTag("Player"))
            {
                Debug.Log("Particle hit the Player!");
        
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>() ?? other.GetComponentInParent<PlayerHealth>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(_damage);
                    Debug.Log($"Enemy electric sword hit Player! Dealt {_damage} damage.");
                }
                else
                {
                    Debug.LogWarning("PlayerHealth component not found on Player!");
                }
            }
            else
            {
                Debug.Log("Particle hit something else: " + other.tag);
            }
        }

    }
}
