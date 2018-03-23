using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_SetCoolDown")]
    public class Enemy_SetCoolDown : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return SetCoolDown(_controller as AI_Enemy);
        }

        bool SetCoolDown(AI_Enemy _AiEnemy)
        {
            if (_AiEnemy.AttackCoolDownTime > 0)
                _AiEnemy.AttackCoolDownTime = _AiEnemy.Enemy.Data.MeleeDamageRate;
            else if (_AiEnemy.AttackCoolDownTime < 0)
                _AiEnemy.AttackCoolDownTime = 0;
            return true;
        }
    }
}