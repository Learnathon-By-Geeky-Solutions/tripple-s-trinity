
using UnityEngine;
using TrippleTrinity.MechaMorph.Health;  
namespace TrippleTrinity.MechaMorph.Enemy
{
    public class GrenadeSpawner : MonoBehaviour
    {
        private int _damage = 2;
        //grenade force
        private void Start()
        {
            Destroy(this.gameObject,3f);
        }



        void OnParticleCollision(GameObject other)
        {
           
            Debug.Log($"OnParticleCollision detected with: {other.name} (Tag: {other.tag})");
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