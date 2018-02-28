using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Wait")]
    public class Wait : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return true;
        }
    }
}

