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
            return true;
        }

        bool SetTarget(Enemy _enemy)
        {
            if (_enemy.IsCharmed)
                GameManager.I.EnemyMng.GetClosestTarget(_enemy);
            return true;
        }
    }
}
