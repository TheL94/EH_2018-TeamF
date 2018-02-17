﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


namespace TeamF
{
    public class ElementalBehaviourThunder : IBulletEffectBehaviour
    {
        Enemy enemy;
        ElementalEffectData elementalData;
        NavMeshAgent navMesh;

        public void DoInit(Enemy _enemy, ElementalEffectData _data)
        {
            enemy = _enemy;
            elementalData = _data;
            navMesh = enemy.GetComponent<NavMeshAgent>();
            navMesh.isStopped = true;
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
            navMesh.isStopped = false;
        }

    }
}