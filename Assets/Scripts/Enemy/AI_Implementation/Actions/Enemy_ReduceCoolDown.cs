using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_ReduceCoolDown")]
    public class Enemy_ReduceCoolDown : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return ReduceCoolDown(_controller as AI_Enemy);
        }

        bool ReduceCoolDown(AI_Enemy _AiEnemy)
        {
            _AiEnemy.AttackCoolDownTime -= Time.deltaTime;
            return true;
        }
    }
}