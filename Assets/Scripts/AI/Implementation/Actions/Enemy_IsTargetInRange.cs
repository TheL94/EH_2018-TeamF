using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_IsTargetInRange")]
    public class Enemy_IsTargetInRange : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return IsTargetInRange((_controller as AI_Enemy).Enemy);
        }

        bool IsTargetInRange(Enemy _enemy)
        {
            if (_enemy.Agent.destination == _enemy.Position)
                return false;
            else if (Vector3.Distance(_enemy.Agent.destination, _enemy.Position) <= _enemy.Agent.stoppingDistance)
                return true;
            else
                return false;
        }
    }
}


