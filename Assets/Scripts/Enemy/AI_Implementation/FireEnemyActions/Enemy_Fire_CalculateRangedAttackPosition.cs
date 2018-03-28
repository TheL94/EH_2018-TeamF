using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_Fire_CalculateRangedAttackPosition")]
    public class Enemy_Fire_CalculateRangedAttackPosition : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            EvaluateRangedAttackPosition((_controller as AI_Enemy).Enemy);
            return true;
        }

        void EvaluateRangedAttackPosition(Enemy _enemy)
        {
            if (Vector3.Distance(_enemy.Agent.destination, _enemy.Target.Position) < 0.3f) 
            {
                Vector3 destination = (_enemy.Target.Position - _enemy.Position).normalized;
                destination *= _enemy.Data.RangedDamageRange + 0.5f;
                _enemy.Agent.destination = destination;
            }
        }
    }
}
