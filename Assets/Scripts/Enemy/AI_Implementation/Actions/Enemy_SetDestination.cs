using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_SetDestination")]
    public class Enemy_SetDestination : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            SetDestination((_controller as AI_Enemy).Enemy);
            return true;
        }

        void SetDestination(Enemy _enemy)
        {
            float attackDistance = _enemy.Data.MeleeDamageRange;
            if(_enemy.Agent.destination != _enemy.Target.Position)
            {
                Vector3 destination = _enemy.Target.Position + new Vector3(Random.Range(-attackDistance / 2, attackDistance / 2), 0f, Random.Range(-attackDistance / 2, attackDistance / 2));
                _enemy.Agent.destination = destination;
            }
        }
    }
}

