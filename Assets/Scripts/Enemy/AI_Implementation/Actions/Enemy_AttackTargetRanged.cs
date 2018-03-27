using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_AttackTargetRanged")]
    public class Enemy_AttackTargetRanged : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            AttackTargetMelee((_controller as AI_Enemy).Enemy);
            return true;
        }

        void AttackTargetMelee(Enemy _enemy)
        {
            
            
        }
    }
}
