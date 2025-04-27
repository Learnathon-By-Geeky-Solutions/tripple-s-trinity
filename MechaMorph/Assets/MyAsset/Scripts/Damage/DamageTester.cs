using UnityEngine;

namespace TrippleTrinity.MechaMorph.Damage
{
    public class DamageTester : MonoBehaviour
    {
        [SerializeField] private Damageable damageable;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P)) // Press P to take damage
            {
                damageable?.TakeDamage(10f);
            }

            if (Input.GetKeyDown(KeyCode.O)) // Press O to heal
            {
                damageable?.Heal(10f);
            }
        }
    }
}