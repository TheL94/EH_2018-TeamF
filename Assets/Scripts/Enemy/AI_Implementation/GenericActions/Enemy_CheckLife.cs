using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_CheckLife")]
    public class Enemy_CheckLife : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return CheckLife((_controller as AI_Enemy).Enemy);
        }

        bool CheckLife(Enemy _enemy)
        {
            if (_enemy.Life <= 0)
                return false;

            return true;
        }
    }
}
