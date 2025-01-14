

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAi : MonoBehaviour
{
    public Rigidbody rb;
    protected NavMeshAgent agent;
    int health;
    public Transform targetPosition;
   public GameObject Bullet;
    public float ProjectileLaunchVelocity = 5f;
    public float MoveRotationSpeed = 45f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        if(targetPosition==null)
        {
            Debug.LogWarning($"(gameObject.name)");
        }
    }

    // Update is called once per frame
   protected virtual void Update()
    {

        if (targetPosition != null) {

            //EnemyAI will move towards the enemy
            MoveTowardsTarget();
            if(agent.isOnOffMeshLink)
            {
                MoveTowardsTarget();
            }
           
        }
    }
    
    protected virtual void MoveTowardsTarget()
    {
        agent.SetDestination(targetPosition.position);
        float remainingDistance = agent.remainingDistance;

    }

    protected virtual void RotateTowardsTarget()
    {
        rb.MoveRotation(Quaternion.Euler(0f, MoveRotationSpeed, 0f));
    }
    protected virtual void Die()
    {
        Debug.Log("Is dead");
        Destroy(gameObject);
    }
    protected virtual void FireProjectile(Vector3 position)
    {
       GameObject BulletProjectile= Instantiate(Bullet,position,transform.rotation);
        if (BulletProjectile != null )
        {
            BulletProjectile.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, ProjectileLaunchVelocity, 0));
            
        }
    }
}
