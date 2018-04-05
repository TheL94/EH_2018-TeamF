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

        protected override void OnInit()
        {
            Enemy = GetComponent<Enemy>();
            IsAttackCoolDown = true;
        }

        #region Fire Pattern
        public int FireConsecutiveAttacks;
        
        public void StartRangedAttackCoolDown(float _time)
        {
            StartCoroutine(RangedAttackCoolDown(_time));
        }

        IEnumerator RangedAttackCoolDown(float _time)
        {
            yield return new WaitForSeconds(_time);
            FireConsecutiveAttacks = 0;
        }
        #endregion

        #region Poison Pattern
        public void StartObscuringCloudLifeTimeCountlDown(float _time, GameObject _cloud)
        {
            StartCoroutine(ObscuringCloudLifeTime(_time, _cloud));
        }

        IEnumerator ObscuringCloudLifeTime(float _time, GameObject _cloud)
        {
            yield return new WaitForSeconds(_time);
            DestroyObject(_cloud);
        }
        #endregion

        #region Enemy Generic
        public bool IsDisengaging;

        #region Attack CoolDown
        public bool IsAttackCoolDown;

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
        #endregion

        #region Paralysis CoolDown
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
        #endregion
    }
}
