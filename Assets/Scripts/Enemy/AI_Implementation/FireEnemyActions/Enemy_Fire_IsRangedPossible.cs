using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_Fire_IsRangedPossible")]
    public class Enemy_Fire_IsRangedPossible : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return IsRangedPossible(_controller as AI_Enemy);
        }

        bool IsRangedPossible(AI_Enemy _AIenemy)
        {
            //if (Vector3.Distance(_enemy.Target.Position, _enemy.Position) <= _enemy.Data.RangedDamageRange - 0.5f)
            if(_AIenemy.IsRangedAttackPossible)
                return true;
            else
                return false;
        }
    }
}
