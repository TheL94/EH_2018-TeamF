using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    public class AI_Enemy : AI_Controller
    {
        public AI_State CharmedState;
        public AI_State ParalizeState;
        public AI_State DamageState;
        public AI_State DeathState;

        public Enemy Enemy { get; private set; }

        public bool IsAttackCoolDown { get; set; }

        public int ConsecutiveAttacks { get; set; }

        protected override void OnInit()
        {
            Enemy = GetComponent<Enemy>();
            IsAttackCoolDown = true;
        }



        #region CoolDowns
        public void StartAttackCoolDown(float _time)
        {
            StartCoroutine(AttackCoolDown(_time));
        }

        IEnumerator AttackCoolDown(float _time)
        {
            IsAttackCoolDown = false;
            yield return new WaitForSeconds(_time);
            IsAttackCoolDown = true;
        }

        public void StartParalysisCoolDown(float _time)
        {
            StartCoroutine(ParalysisCoolDown(_time));
        }

        IEnumerator ParalysisCoolDown(float _time)
        {
            Enemy.IsParalized = true;
            yield return new WaitForSeconds(_time);
            Enemy.IsParalized = false;
        }
        #endregion
    }
}
