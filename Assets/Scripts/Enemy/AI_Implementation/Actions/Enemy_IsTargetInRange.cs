using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

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
            if (Vector3.Distance(_enemy.Target.Position, _enemy.Position) <= _enemy.Data.MeleeDamageRange)
                return true;
            else
                return false;
        }
    }
}


