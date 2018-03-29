using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_IsTargetInRangedRange")]
    public class Enemy_IsTargetInRangedRange : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return IsTargetInRangedRange((_controller as AI_Enemy).Enemy);
        }

        bool IsTargetInRangedRange(Enemy _enemy)
        {
            if (Vector3.Distance(_enemy.Target.Position, _enemy.Position) <= _enemy.Data.RangedDamageRange + 0.25f)
                return true;
            else
                return false;
        }
    }
}
