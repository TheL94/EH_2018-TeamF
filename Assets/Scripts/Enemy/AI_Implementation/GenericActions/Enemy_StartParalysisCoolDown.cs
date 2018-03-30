using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_StartParalysisCoolDown")]
    public class Enemy_StartParalysisCoolDown : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            StartParalysisCoolDown(_controller as AI_Enemy);
            return true;
        }

        void StartParalysisCoolDown(AI_Enemy _AIEnemy)
        {
            if (_AIEnemy.IsAttackCoolDown)
                _AIEnemy.StartParalysisCoolDown(_AIEnemy.Enemy.Data.MeleeDamageRate);
        }
    }
}