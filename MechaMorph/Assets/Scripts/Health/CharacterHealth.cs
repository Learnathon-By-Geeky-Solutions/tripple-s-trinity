using UnityEngine;

namespace TrippleTrinity.MechaMorph.Health
{
    public class CharacterHealth : MonoBehaviour
    {
        [SerializeField] private float health = 100f;

        public void TakeDamage(float amount)
        {
            health -= amount;
            if (health <= 0)
            {
                Die(gameObject);
            }
        }

        private static void Die(GameObject target)
        {
            Destroy(target);
        }
    }
}