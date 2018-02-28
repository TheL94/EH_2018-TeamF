using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TeamF.Old
{
    public class Enemy : MonoBehaviour
    {      
        float attackTimeCounter;

        //private void FixedUpdate()
        //{
        //    if (target == null)
        //        return;

        //    if (target.Life > 0)
        //    {
        //        Move();
        //        Attack(); 
        //    }
        //    else
        //        ChangeMyTarget();

        //    CheckMovementConstrains();
        //}

        //void Move()
        //{
        //    agentTimeCounter += Time.deltaTime;
        //    if (agentTimeCounter >= 0.3f)
        //    {
        //        agent.SetDestination(target.Position);
        //        agentTimeCounter = 0;
        //    }
        //}

        void RotateTowards(Vector3 _pointToLook)
        {
            transform.rotation = Quaternion.LookRotation(_pointToLook - transform.position, Vector3.up);
        }

        void CheckMovementConstrains()
        {
            if (transform.rotation.x != 0 || transform.rotation.z != 0)
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        ///// <summary>
        ///// provoca danno alla vita del target se è alla distanza corretta
        ///// </summary>
        //void Attack()
        //{
        //    attackTimeCounter += Time.deltaTime;
        //    if (attackTimeCounter >= data.DamageRate)
        //    {
        //        if (Vector3.Distance(agent.destination, transform.position) <= agent.stoppingDistance)
        //        {
        //            if (target.Life > 0)
        //            {
        //                RotateTowards(target.Position);
        //                CurrentBehaviour.DoAttack();
        //                attackTimeCounter = 0;
        //            }
        //        }
        //    }
        //}
    }
}