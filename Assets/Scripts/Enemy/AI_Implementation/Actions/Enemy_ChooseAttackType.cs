using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_ChooseAttackType")]
    public class Enemy_ChooseAttackType : AI_Action
    {
        public AttackType HighestPriorityAttack;

        protected override bool Act(AI_Controller _controller)
        {
            return true;
        }
    }
}