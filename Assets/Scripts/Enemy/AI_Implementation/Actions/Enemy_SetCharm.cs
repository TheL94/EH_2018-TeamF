using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_SetCharm")]
    public class Enemy_SetCharm : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return SetCharm((_controller as AI_Enemy).Enemy);
        }

        bool SetCharm(Enemy _enemy)
        {
            _enemy.Target = null;
            return true;
        }
    }
}