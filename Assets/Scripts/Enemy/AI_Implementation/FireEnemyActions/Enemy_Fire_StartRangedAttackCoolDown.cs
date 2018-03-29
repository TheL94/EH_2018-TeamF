using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_Fire_StartRangedAttackCoolDown")]
    public class Enemy_Fire_StartRangedAttackCoolDown : AI_Action
    {
        float RangedAttackDuration;

        protected override bool Act(AI_Controller _controller)
        {
            StartRangedAttackCoolDown(_controller as AI_Enemy);
            return true;
        }

        void StartRangedAttackCoolDown(AI_Enemy _AIenemy)
        {
            _AIenemy.StartRangedAttackCoolDown(RangedAttackDuration);
        }
    }
}