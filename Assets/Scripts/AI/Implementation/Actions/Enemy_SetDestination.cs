using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_SetDestination")]
    public class Enemy_SetDestination : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return SetDestination((_controller as AI_Enemy).Enemy);
        }

        bool SetDestination(Enemy _enemy)
        {
            if (_enemy.Target == null || _enemy.Target.Life <= 0)
                return false;

            _enemy.Agent.SetDestination(_enemy.Target.Position);
            return true;
        }
    }
}

