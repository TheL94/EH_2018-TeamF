using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_ChangeAnimationState")]
    public class Enemy_ParalysisAnimation : AI_Action
    {

        public bool SetOnlyMovementAnim;
        public bool IsWalking;
        public int AnimationState;

        protected override bool Act(AI_Controller _controller)
        {
            return ChangeAnimationState((_controller as AI_Enemy).Enemy);
        }

        bool ChangeAnimationState(Enemy _enemy)
        {
            if (_enemy.IsParalized)
            {
                if (_enemy.Animator != null)
                {
                    if (SetOnlyMovementAnim)
                        _enemy.Animator.SetBool("IsWalking", IsWalking);
                    else
                        _enemy.Animator.SetInteger("State", AnimationState);
                } 
            }
            return true;
        }
    }
}