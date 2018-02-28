using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.AI;

namespace TeamF.AI
{
    public class AI_Enemy : AI_Controller
    {
        public Enemy Enemy { get; private set; }

        public AI_State StartState;

        protected override void OnInit()
        {
            Enemy = GetComponent<Enemy>();
        }
    }
}

