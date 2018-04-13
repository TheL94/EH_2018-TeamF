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
            float targetDistance = Vector3.Distance(_AIenemy.Enemy.Target.Position, _AIenemy.Enemy.Position);
            if (targetDistance >= _AIenemy.Enemy.Data.RangedDamageRange - _AIenemy.Enemy.Data.StoppingDistance)
            {
                _AIenemy.IsDisengaging = false;
                return true;
            }
            else
                return false;
        }
    }
}
