using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_Freeze")]
    public class Enemy_Freeze : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return Freeze((_controller as AI_Enemy).Enemy);
        }

        bool Freeze(Enemy _enemy)
        {
            _enemy.IsParalized = true;
            return true;
        }
    }
}