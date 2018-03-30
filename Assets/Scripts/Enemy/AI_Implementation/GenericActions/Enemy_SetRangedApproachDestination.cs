using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_SetRangedApproachDestination")]
    public class Enemy_SetRangedApproachDestination : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            SetRangedApproachDestination((_controller as AI_Enemy).Enemy);
            return true;
        }

        void SetRangedApproachDestination(Enemy _enemy)
        {
            float attackDistance = _enemy.Data.RangedDamageRange;
            if(_enemy.Agent.destination != _enemy.Target.Position)
            {
                Vector3 destination = _enemy.Target.Position + new Vector3(Random.Range(-attackDistance , attackDistance), 0f, Random.Range(-attackDistance , attackDistance));
                _enemy.Agent.destination = destination;
            }
        }
    }
}

