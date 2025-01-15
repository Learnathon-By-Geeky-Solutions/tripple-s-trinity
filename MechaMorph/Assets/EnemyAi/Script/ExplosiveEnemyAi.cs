using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ExplosiveEnemyAi : EnemyAi
{
    
    
    private bool hasExploded = false;
    
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
     
        if (!hasExploded && AutoBreak())
        {
            hasExploded = true;
            Explode();
        }
    }
    bool AutoBreak()
    {
        if (agent.remainingDistance <= 5f && agent.velocity.magnitude > 0.1f)
        {
           agent.autoBraking = true;
            return true;
        }
        return false;
    }
    void Explode()
    {
        ////ExplosionSound();
        Debug.Log("Death");
        GameObject.Destroy(this.gameObject);
    }
    void ExplosionSound()
    {

    }
}
