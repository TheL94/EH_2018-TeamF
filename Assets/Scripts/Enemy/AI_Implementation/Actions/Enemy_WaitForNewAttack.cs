using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_WaitForNewAttack")]
    public class Enemy_WaitForNewAttack : AI_Action
    {
        float timeCounter;

        protected override bool Act(AI_Controller _controller)
        {
            return WaitForNewAttack((_controller as AI_Enemy).Enemy);
        }

        bool WaitForNewAttack(Enemy _enemy)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= _enemy.Data.MeleeDamageRate)
            {
                timeCounter = 0;
                return true;
            }
            else
                return false;
        }
    }
}

