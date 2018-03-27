using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_AttackTargetMelee")]
    public class Enemy_AttackTargetMelee : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return AttackTargetMelee((_controller as AI_Enemy).Enemy);
        }

        bool AttackTargetMelee(Enemy _enemy)
        {
            _enemy.Target.TakeDamage(_enemy.Data.MeleeDamage);
            return true;
        }
    }
}

