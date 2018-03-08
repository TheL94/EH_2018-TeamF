using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/DelayAction")]
    public class DelayAction : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return true;
        }
    }
}

