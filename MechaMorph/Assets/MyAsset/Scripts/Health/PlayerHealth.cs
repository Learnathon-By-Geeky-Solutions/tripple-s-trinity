using UnityEngine;
using TrippleTrinity.MechaMorph.Ability;
using TrippleTrinity.MechaMorph.Token;
using TrippleTrinity.MechaMorph.Damage;
using TrippleTrinity.MechaMorph.SoundManager_main.SoundManager_main;
namespace TrippleTrinity.MechaMorph.Health
{
    public class PlayerHealth : Damageable
    {
        public enum PlayerForm { Ball, Robot }

        [SerializeField] private PlayerForm currentForm = PlayerForm.Ball;
        [SerializeField] private bool shouldDropToken;
        private TokenSpawner _tokenSpawner;
        private AreaDamageAbility _areaDamageAbility;


        public override void TakeDamage(float amount)
        {
            if (currentForm == PlayerForm.Ball)
            {
                amount *= 0.5f; // Ball form takes reduced damage
            }

            base.TakeDamage(amount);
        }

        protected override void HandleDeath()
        {
            //Player Deatg Sound
            SoundManager.PlaySound(SoundType.PlayerDeath);
            base.HandleDeath();
            Debug.Log("Player has died!");

            // Handle player-specific death logic here, if any (e.g., game over, respawn)
        }

        public void SwitchForm(PlayerForm newForm)
        {
            currentForm = newForm;
        }
    }
}