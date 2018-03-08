using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_ChangeAnimationState")]
    public class Enemy_ChangeAnimationState : AI_Action
    {
        public Enemy.AnimationState AnimationState;

        protected override bool Act(AI_Controller _controller)
        {
            (_controller as AI_Enemy).Enemy.AnimState = AnimationState;
            return true;
        }
    }
}
