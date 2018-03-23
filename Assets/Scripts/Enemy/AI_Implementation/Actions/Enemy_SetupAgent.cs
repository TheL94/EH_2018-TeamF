using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_SetupAgent")]
    public class Enemy_SetupAgent : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            SetupAgent((_controller as AI_Enemy).Enemy);
            return true;
        }

        void SetupAgent(Enemy _enemy)
        {
            _enemy.Agent.speed = _enemy.Data.Speed;
            _enemy.Agent.stoppingDistance = 1.5f; 
        }
    }
}
