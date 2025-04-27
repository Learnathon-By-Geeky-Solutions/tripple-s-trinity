using UnityEngine;
using TrippleTrinity.MechaMorph.Health;  

namespace TrippleTrinity.MechaMorph.Enemy
{
    public class MeeleEnemyDamage : MonoBehaviour
    {
        private readonly int _damage = 3;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log(other.gameObject.name);

                PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>() 
                                            ?? other.gameObject.GetComponentInParent<PlayerHealth>();

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
        }
    }
}