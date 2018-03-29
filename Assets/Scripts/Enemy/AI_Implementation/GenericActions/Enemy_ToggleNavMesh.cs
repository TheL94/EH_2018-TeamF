using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_ToggleNavMesh")]
    public class Enemy_ToggleNavMesh : AI_Action
    {
        public bool Stop;

        protected override bool Act(AI_Controller _controller)
        {
            StopNavMesh((_controller as AI_Enemy).Enemy);
            return true;
        }

        void StopNavMesh(Enemy _enemy)
        {
            if (_enemy.Agent != null)
            {
                if (Stop && _enemy.Agent.speed > 0f)
                    _enemy.Agent.speed = 0f;
                else if(_enemy.Agent.speed == 0f)
                    _enemy.Agent.speed = _enemy.Data.Speed;
            }
        }
    }
}
