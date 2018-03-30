using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_KeepEngageDistance")]
    public class Enemy_KeepEngageDistance : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            KeepCurrentDistance((_controller as AI_Enemy).Enemy);
            return true;
        }

        void KeepCurrentDistance(Enemy _enemy)
        {
            if (Vector3.Distance(_enemy.Target.Position, _enemy.Position) >= _enemy.Data.RangedDamageRange + 0.25f)
            {
                Vector3 engagePosition = (_enemy.Target.Position - _enemy.Position);
                float engageDistance = engagePosition.magnitude - _enemy.Data.RangedDamageRange;
                engagePosition = engagePosition.normalized * (engageDistance + 0.25f);
                _enemy.Agent.destination = _enemy.Position + engagePosition;
            }         
        }
    }
}
