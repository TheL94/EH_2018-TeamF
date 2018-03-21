using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_RestoreCoolDown")]
    public class Enemy_RestoreCoolDown : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return RestoreCoolDown(_controller as AI_Enemy);
        }

        bool RestoreCoolDown(AI_Enemy _AiEnemy)
        {
            _AiEnemy.AttackCoolDownTime = _AiEnemy.Enemy.Data.MeleeDamageRate;
            return true;
        }
    }
}