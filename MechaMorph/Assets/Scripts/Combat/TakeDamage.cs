using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public float enemyHealth = 50f;

    public void Damage(float amount)
    {
        enemyHealth -= amount;
        if (enemyHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
