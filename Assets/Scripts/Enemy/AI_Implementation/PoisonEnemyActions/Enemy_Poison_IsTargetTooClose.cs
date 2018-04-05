using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_Poison_IsTargetTooClose")]
    public class Enemy_Poison_IsTargetTooClose : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return IsTargetTooClose((_controller as AI_Enemy).Enemy);
        }

        bool IsTargetTooClose(Enemy _enemy)
        {
            EnemyPoisonData data = _enemy.Data as EnemyPoisonData;
            if (Vector3.Distance(_enemy.Target.Position, _enemy.Position) <= data.CloudReleaseDistance)
                return true;
            else
            {
                _enemy.AI_Enemy.IsDisengaging = false;
                return false;
            }
        }
    }
}

