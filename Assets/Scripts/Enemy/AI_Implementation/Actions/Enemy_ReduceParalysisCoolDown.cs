using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_ReduceParalysisCoolDown")]
    public class Enemy_ReduceParalysisCoolDown : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return ReduceParalysisCoolDown((_controller as AI_Enemy));
        }

        bool ReduceParalysisCoolDown(AI_Enemy _enemy)
        {
            _enemy.ParalysisCoolDownTime -= Time.deltaTime;
            return true;
        }
    }
}