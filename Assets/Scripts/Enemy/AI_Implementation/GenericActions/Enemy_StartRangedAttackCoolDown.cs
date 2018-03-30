using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_StartRangedAttackCoolDown")]
    public class Enemy_StartRangedAttackCoolDown : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            StartRangedAttackCoolDown(_controller as AI_Enemy);
            return true;
        }

        void StartRangedAttackCoolDown(AI_Enemy _AIEnemy)
        {
            if(_AIEnemy.IsAttackCoolDown)
                _AIEnemy.StartAttackCoolDown(_AIEnemy.Enemy.Data.RangedDamageRate);
        }
    }
}