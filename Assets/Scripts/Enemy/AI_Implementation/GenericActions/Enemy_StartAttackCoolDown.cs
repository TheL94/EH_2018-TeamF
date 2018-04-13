using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_StartAttackCoolDown")]
    public class Enemy_StartAttackCoolDown : AI_Action
    {
        public AttackType AttackType;

        protected override bool Act(AI_Controller _controller)
        {
            StartRangedAttackCoolDown((_controller as AI_Enemy).Enemy);
            return true;
        }

        void StartRangedAttackCoolDown(Enemy _enemy)
        {
            float damageRate = 0f;

            switch (AttackType)
            {
                case AttackType.Melee:
                    damageRate = _enemy.Data.MeleeDamageRate;
                    break;
                case AttackType.Ranged:
                    damageRate = _enemy.Data.RangedDamageRate;
                    break;
            }

            if (_enemy.AI_Enemy.IsAttackCoolDown)
                _enemy.AI_Enemy.StartAttackCoolDown(damageRate);
        }
    }
}