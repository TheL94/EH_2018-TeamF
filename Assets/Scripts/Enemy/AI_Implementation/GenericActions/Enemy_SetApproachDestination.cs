﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_SetApproachDestination")]
    public class Enemy_SetApproachDestination : AI_Action
    {
        public AttackType AttackType;

        protected override bool Act(AI_Controller _controller)
        {
            SetRangedApproachDestination((_controller as AI_Enemy).Enemy);
            return true;
        }

        void SetRangedApproachDestination(Enemy _enemy)
        {
            float damageRange = 0f;

            switch (AttackType)
            {
                case AttackType.Melee:
                    damageRange = _enemy.Data.MeleeDamageRange;
                    break;
                case AttackType.Ranged:
                    damageRange = _enemy.Data.RangedDamageRange;
                    break;
            }

            if (Vector3.Distance(_enemy.Position, _enemy.Target.Position) > damageRange) 
            {
                Vector3 engagePosition = (_enemy.Target.Position - _enemy.Position);
                float engageDistance = engagePosition.magnitude - damageRange;
                
                if(engageDistance > damageRange)
                    engageDistance += Random.Range(0.05f, _enemy.Data.RangeOffset); // se il nemico è fuori dalla distanza di attacco
                else
                    engageDistance -= Random.Range(0.05f, _enemy.Data.RangeOffset); // se il nemico è dentro la distanza di attacco               

                engagePosition = engagePosition.normalized * engageDistance;
                engagePosition += _enemy.Position;

                _enemy.Agent.destination = engagePosition;               
            }
        }
    }
}

