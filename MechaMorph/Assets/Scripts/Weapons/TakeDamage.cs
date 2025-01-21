using UnityEngine;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class TakeDamage : MonoBehaviour
    {
        [SerializeField] private float enemyHealth = 50f;
        

        public void Damage(float amount)
        {
            enemyHealth -= amount;
            if (enemyHealth <= 0f)
            {
                Die(gameObject);
            }
        }

        static void Die(GameObject obj)
        {
            Destroy(obj);
        }
    }
}
