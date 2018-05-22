using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_Die")]
    public class Enemy_Die : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            Die((_controller as AI_Enemy).Enemy);
            return true;
        }

        void Die(Enemy _enemy)
        {
            Animator anim = _enemy.GetComponentInChildren<Animator>();

            if(anim != null)
                anim.Play("Idle");

            if (Enemy.EnemyDeath != null)
                Enemy.EnemyDeath(_enemy);
        }
    }
}
