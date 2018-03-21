using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    public class AI_Enemy : AI_Controller
    {
        public Enemy Enemy { get; private set; }

        public float AttackCoolDownTime { get; set; }
        public float ParalysisCoolDownTime { get; set; }

        public AI_State CharmedState;
        public AI_State ParalizeState;
        public AI_State DamageState;
        public AI_State DeadState;

        protected override void OnInit()
        {
            Enemy = GetComponent<Enemy>();
        }
    }
}

