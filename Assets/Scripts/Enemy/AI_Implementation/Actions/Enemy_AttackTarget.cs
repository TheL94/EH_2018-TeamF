using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_AttackTarget")]
    public class Enemy_AttackTarget : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return AttackTarget((_controller as AI_Enemy).Enemy);
        }

        bool AttackTarget(Enemy _enemy)
        {
            _enemy.Target.TakeDamage(_enemy.Data.MeleeDamage);
            return true;
        }
    }
}

