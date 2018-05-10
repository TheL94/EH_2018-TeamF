using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Animation/Enemy_ParalysisAnimationState")]
    public class Enemy_ParalysisAnimationState : AI_Action
    {
        public int AnimationState;

        protected override bool Act(AI_Controller _controller)
        {
            return ChangeAnimationState((_controller as AI_Enemy).Enemy);
        }

        bool ChangeAnimationState(Enemy _enemy)
        {
            if (_enemy.Animator != null)
            {
                if (_enemy.IsParalyzed)
                {
                    _enemy.Animator.SetInteger("State", AnimationState); 
                }
            }
            return true;
        }
    }
}
