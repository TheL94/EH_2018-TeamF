using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_Fire_CountAttackNumber")]
    public class Enemy_Fire_CountAttackNumber : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            CountAttackNumber(_controller as AI_Enemy);
            return true;
        }

        void CountAttackNumber(AI_Enemy _AIenemy)
        {
            _AIenemy.FireConsecutiveAttacks++;
        }
    }
}
