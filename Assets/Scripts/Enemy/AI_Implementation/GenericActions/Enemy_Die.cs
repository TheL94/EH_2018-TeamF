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
            // TODO : risolto bug in modo scoretto --------------------
            EffectController effect = _enemy.GetComponent<EffectController>();
            if (effect != null)
                effect.gameObject.SetActive(false);
            // --------------------------------------------------------

            _enemy.GetComponentInChildren<ParticlesController>().StopAllParticles();
            _enemy.GetComponentInChildren<Animator>().Play("Idle");
            _enemy.GetComponentInChildren<BlinkController>().ResetEffects();

            if (Enemy.EnemyDeath != null)
                Enemy.EnemyDeath(_enemy);
            _enemy.AI_Enemy.IsActive = false;
        }
    }
}
