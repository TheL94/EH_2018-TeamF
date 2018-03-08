using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    public class AI_Enemy : AI_Controller
    {
        public Enemy Enemy { get; private set; }

        protected override void OnInit()
        {
            Enemy = GetComponent<Enemy>();
        }
    }
}

