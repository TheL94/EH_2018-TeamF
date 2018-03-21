using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_SetTarget")]
    public class Enemy_SetTarget : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return SetTarget((_controller as AI_Enemy).Enemy);
        }


        bool SetTarget(Enemy _enemy)
        {
            if (_enemy.Target != null)
                return true;

            if (_enemy.IsCharmed)
            {
                _enemy.Target = GameManager.I.EnemyMng.GetClosestTarget(_enemy);
                return true;
            }
            else
            {
                _enemy.Target = GameManager.I.Player.Character;
                if (_enemy.Target != null)
                    return true;

                return false;
            }
        }
    }
}
