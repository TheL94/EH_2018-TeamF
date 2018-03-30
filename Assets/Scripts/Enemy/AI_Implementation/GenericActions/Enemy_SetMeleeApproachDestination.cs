using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_SetMeleeApproachDestination")]
    public class Enemy_SetMeleeApproachDestination : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            SetMeleeApproachDestination((_controller as AI_Enemy).Enemy);
            return true;
        }

        void SetMeleeApproachDestination(Enemy _enemy)
        {
            float attackDistance = _enemy.Data.MeleeDamageRange;
            if(_enemy.Agent.destination != _enemy.Target.Position)
            {
                Vector3 destination = _enemy.Target.Position + new Vector3(Random.Range(-attackDistance , attackDistance), 0f, Random.Range(-attackDistance , attackDistance));
                _enemy.Agent.destination = destination;
            }
        }
    }
}

