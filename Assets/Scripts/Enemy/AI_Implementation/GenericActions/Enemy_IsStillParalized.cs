using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_IsStillParalized")]
    public class Enemy_IsStillParalized : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return IsStillParalized((_controller as AI_Enemy).Enemy); 
        }

        bool IsStillParalized(Enemy _enemy)
        {
            if(_enemy.IsParalized)
                return true;

            return false;
        }
    }
}