using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_Fire_IsMeleePossible")]
    public class Enemy_Fire_IsMeleePossible : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return IsMeleePossible(_controller as AI_Enemy);
        }

        bool IsMeleePossible(AI_Enemy _AIenemy)
        {
            if (_AIenemy.FireConsecutiveAttacks < (_AIenemy.Enemy.Data as EnemyFireData).MeleeConsecutiveAttacks)
                return true;
            else
                return false;
        }
    }
}
