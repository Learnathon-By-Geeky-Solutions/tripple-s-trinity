using Ability;
using UnityEngine;

namespace token
{
    public class TokenCollect : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Notify AreaDamageAbility about the token collection
                AreaDamageAbility ability = other.GetComponent<AreaDamageAbility>();
                if (ability != null)
                {
                    ability.CollectToken();
                    Debug.Log("Token collected by player!");
                }
                else
                {
                    Debug.LogWarning("AreaDamageAbility component not found on player!");
                }

                // Destroy the token
                Destroy(gameObject);
            }
        }
    }
}