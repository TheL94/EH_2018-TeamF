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
            (_controller as AI_Enemy).IsActive = false;
            return true;
        }
    }
}

