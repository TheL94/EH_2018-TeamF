using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_IsCoolDown")]
    public class Enemy_IsCoolDown : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return CheckCoolDown(_controller as AI_Enemy);
        }

        bool CheckCoolDown(AI_Enemy _AiEnemy)
        {
            if(_AiEnemy.AttackCoolDownTime <= 0)
                return true;

            return false;
        }
    }
}
