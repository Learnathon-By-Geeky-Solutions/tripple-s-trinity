using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAi : EnemyAi
{
    /*    public Transform Transform;
        public Vector3 position;
        public float speed=5f;
        public float angularSpeed = 20f;
       */

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        //check if within attack range


        //if (transform != null)
        //{
        //    Vector3 Distance = (Transform.position - position).normalized;

        //    transform.position += Distance * speed * Time.deltaTime;
            
        //    //Quaternion lookRotation= Quaternion.LookRotation(Distance);
        //    //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime);
        //}

    }
}
