using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_FindTarget")]
    public class Enemy_FindTarget : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return SetTarget((_controller as AI_Enemy).Enemy);
        }

        bool SetTarget(Enemy _enemy)
        {
            if (GameManager.I.EnemyMng.IgnoreTarget)
            {
                _enemy.Target = null;
                return false;
            }

            if (_enemy.Target != null && _enemy.Target.Life > 0)
                return true;

            _enemy.Target = GameManager.I.EnemyMng.GetTarget(_enemy);

            if (_enemy.Target != null)
                return true;
            else
                return false;
        }
    }
}
