using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_IsTargetInRange")]
    public class Enemy_IsTargetInRange : AI_Action
    {
        public AttackType AttackType;

        protected override bool Act(AI_Controller _controller)
        {
            return IsTargetInRangedRange((_controller as AI_Enemy).Enemy);
        }

        bool IsTargetInRangedRange(Enemy _enemy)
        {
            float damageRange = 0f;

            switch (AttackType)
            {
                case AttackType.Melee:
                    damageRange = _enemy.Data.MeleeDamageRange;
                    break;
                case AttackType.Ranged:
                    damageRange = _enemy.Data.RangedDamageRange;
                    break;
            }

            if (Vector3.Distance(_enemy.Target.Position, _enemy.Position) <= damageRange + _enemy.Data.RangeOffset)
                return true;
            else
                return false;
        }
    }
}
