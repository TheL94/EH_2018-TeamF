using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_IsTargetAvailable")]
    public class Enemy_IsTargetAvailable : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return IsTargetAvailable((_controller as AI_Enemy).Enemy);
        }

        bool IsTargetAvailable(Enemy _enemy)
        {
            if (_enemy.Target == null || _enemy.Target.Life <= 0)
                return false;
            else
                return true;
        }
    }
}

