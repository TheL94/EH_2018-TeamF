using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_CalculateDisengagePosition")]
    public class Enemy_CalculateDisengagePosition : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            CalculateRangedAttackPosition((_controller as AI_Enemy).Enemy);
            return true;
        }

        void CalculateRangedAttackPosition(Enemy _enemy)
        {
            if (!_enemy.AI_Enemy.IsDisengaging) 
            {
                _enemy.AI_Enemy.IsDisengaging = true;
                Vector3 disengagePosition = _enemy.Target.Position - _enemy.Position;
                float disengageDistance = disengagePosition.magnitude - _enemy.Data.RangedDamageRange;

                disengagePosition = disengagePosition.normalized * _enemy.Data.RangedDamageRange;
                disengagePosition += _enemy.Target.Position;
                disengagePosition.y = 0f;

                _enemy.Agent.destination = disengagePosition;
                if (_enemy.Agent.isActiveAndEnabled)
                    _enemy.Agent.isStopped = false;
            }
            else
            {
                if(Vector3.Distance(_enemy.Position, _enemy.AI_Enemy.CurrentDestination) <= _enemy.Data.StoppingDistance)
                {
                    if (_enemy.Agent.isActiveAndEnabled)
                        _enemy.Agent.isStopped = true;
                }
            }
        }
    }
}
