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
            return IsStillParalized(_controller as AI_Enemy); 
        }

        bool IsStillParalized(AI_Enemy _enemy)
        {
            if(_enemy.ParalysisCoolDownTime <= 0)
            {
                _enemy.Enemy.IsParalized = false;
                return false;
            }

            return true;
        }
    }
}