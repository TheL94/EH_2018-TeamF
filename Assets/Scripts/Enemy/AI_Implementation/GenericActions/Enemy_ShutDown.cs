using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_ShutDown")]
    public class Enemy_ShutDown : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            ShutDown((_controller as AI_Enemy).Enemy);
            return true;
        }

        void ShutDown(Enemy _enemy)
        {
            _enemy.GetComponent<Collider>().enabled = false;

            if(GameManager.I.CurrentState == FlowState.Gameplay)
                GameManager.I.AudioMng.PlaySound(Clips.EnemyDeath);

            _enemy.GetComponentInChildren<ParticlesController>().StopAllParticles();
            _enemy.GetComponentInChildren<BlinkController>().ResetEffects();

            EffectController effect = _enemy.GetComponent<EffectController>();
            if (effect != null)
                effect.StopAllEffects();

            if (Enemy.UpdateKill != null)
                Enemy.UpdateKill(_enemy);
        }
    }
}