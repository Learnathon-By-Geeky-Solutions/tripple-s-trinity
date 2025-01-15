using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionProjectile : EnemyAi
{
    // Start is called before the first frame update

    public float delayTime = 2f;
    protected override void Start()
    {
       StartCoroutine(FireProjectileAfterDelay());
    }
    // Update is called once per frame
    protected override  void Update()
    {
 
            
     
    }

    IEnumerator FireProjectileAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        Debug.Log("Bullet is Fired");
        FireProjectile(transform.position);
    }

}
