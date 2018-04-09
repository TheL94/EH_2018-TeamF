using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_StartMeleeAttackCoolDown")]
    public class Enemy_StartMeleeAttackCoolDown : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            StartCoolDown(_controller as AI_Enemy);
            return true;
        }

        void StartCoolDown(AI_Enemy _AIEnemy)
        {
            if(_AIEnemy.IsAttackCoolDown)
                _AIEnemy.StartAttackCoolDown(_AIEnemy.Enemy.Data.MeleeDamageRate);
        }
    }
}