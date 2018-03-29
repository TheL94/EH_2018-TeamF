using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_ChangeAnimationState")]
    public class Enemy_ChangeAnimationState : AI_Action
    {
        public Enemy.AnimationState AnimationState;

        protected override bool Act(AI_Controller _controller)
        {        
            return ChangeAnimationState((_controller as AI_Enemy).Enemy);
        }

        bool ChangeAnimationState(Enemy _enemy)
        {
            if (_enemy.Animator != null)
                _enemy.AnimState = AnimationState;
            return true;
        }
    }
}
