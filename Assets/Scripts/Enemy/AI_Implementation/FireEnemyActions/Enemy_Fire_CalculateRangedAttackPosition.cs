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
            if (!_enemy.AI_Enemy.FireIsDisengaging) 
            {
                _enemy.AI_Enemy.FireIsDisengaging = true;
                   Vector3 disengageDestination = (_enemy.Target.Position - _enemy.Position).normalized;
                disengageDestination *= _enemy.Data.RangedDamageRange + 0.5f;
                _enemy.Agent.destination = _enemy.Target.Position + disengageDestination;
            }
        }
    }
}
