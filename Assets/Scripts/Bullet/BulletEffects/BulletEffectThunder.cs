using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


namespace TeamF
{
    public class BulletEffectThunder : IElementalEffectBehaviour
    {
        IEffectable target;
        ElementalEffectData elementalData;
        //NavMeshAgent navMesh;

        public void DoInit(IEffectable _target, ElementalEffectData _data)
        {
            target = _target;
            elementalData = _data;
            target.SetParalisys(true);
            //navMesh = target.GetComponent<NavMeshAgent>();
            //if (navMesh.isActiveAndEnabled)
            //{
            //    navMesh.isStopped = true; 
            //}
        }

        public bool DoUpdate()
        {
            elementalData.TimeOfEffect -= Time.deltaTime;
            if(elementalData.TimeOfEffect <= 0)
            {
                return true;
            }
            return false;
        }

        public void DoStopEffect()
        {
            target.SetParalisys(false);

            //if (navMesh != null)
            //{
            //    if (navMesh.isActiveAndEnabled)
            //    {
            //        navMesh.isStopped = false;
            //    } 
            //}
        }

    }
}