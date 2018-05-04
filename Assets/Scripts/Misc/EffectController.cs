using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EffectController : MonoBehaviour
    {
        public float ParalysisImmunityTime;
        bool canBeParalyzed = true;
        int runningCorutineNumber;

        bool isBulletBehaviourInitialized;
        bool isComboInitialized;

        IElementalEffectBehaviour bulletBehaviour;
        IElementalEffectBehaviour comboBehaviour;

        void Update()
        {
            if (isBulletBehaviourInitialized)
            {
                if (bulletBehaviour.DoUpdate())
                    StopBulletEffect();
            }
            if (isComboInitialized)
            {
                if(comboBehaviour.DoUpdate())
                    StopComboEffect();
            }
        }

        /// <summary>
        /// Set the time of effect with the float passed
        /// </summary>
        /// <param name="_timeOfEffect"></param>
        public void InitEffect(IElementalEffectBehaviour _behaviour, IEffectable _target, ElementalEffectData _elementalData, bool _isComboEffect = false)
        {
            if (!_isComboEffect)
            {
                if (isBulletBehaviourInitialized)
                    bulletBehaviour.DoStopEffect();

                bulletBehaviour = _behaviour;
                if (CheckIfEffectCanBeApplied(_behaviour, _target))
                {
                    bulletBehaviour.DoInit(_target, _elementalData);
                    isBulletBehaviourInitialized = true;
                } 
            }
            else
            {
                if (isComboInitialized)
                    comboBehaviour.DoStopEffect();

                comboBehaviour = _behaviour;
                comboBehaviour.DoInit(_target, _elementalData);
                isComboInitialized = true;
            }
        }

        void StopBulletEffect()
        {
            bulletBehaviour.DoStopEffect();
            isBulletBehaviourInitialized = false;

            ApplyParalysisImmunity(bulletBehaviour);
        }

        void StopComboEffect()
        {
            comboBehaviour.DoStopEffect();
            isComboInitialized = false;

            ApplyParalysisImmunity(comboBehaviour);
        }

        void ApplyParalysisImmunity(IElementalEffectBehaviour _behaviour)
        {
            if (_behaviour.GetType() == typeof(ParalyzeEffect))
            {
                if (runningCorutineNumber > 0)
                    StopAllCoroutines();

                StartCoroutine(ParalyzeImmunity(ParalysisImmunityTime));
            }
        }

        /// <summary>
        /// Funzione che ritorna true se l'effetto elementale può essere applicato , altrimenti ritorna false
        /// </summary>
        /// <param name="_behaviour"></param>
        /// <param name="_target"></param>
        /// <returns></returns>
        bool CheckIfEffectCanBeApplied(IElementalEffectBehaviour _behaviour, IEffectable _target)
        {
            if (_behaviour.GetType() == typeof(ParalyzeEffect) && !canBeParalyzed)
                return false;

            if (typeof(Enemy).IsAssignableFrom(_target.GetType()))
            {
                Enemy enemy = _target as Enemy;

                if (_behaviour.GetType() == typeof(SetOnFireEffect) && enemy.CurrentBehaviour.GetType() == typeof(EnemyFireBehaviour))
                    return false;
                if (_behaviour.GetType() == typeof(PoisonedEffect) && enemy.CurrentBehaviour.GetType() == typeof(EnemyPoisonBehaviour))
                    return false;
                if (_behaviour.GetType() == typeof(SlowingEffect) && enemy.CurrentBehaviour.GetType() == typeof(EnemyWaterBehaviour))
                    return false;
                if (_behaviour.GetType() == typeof(ParalyzeEffect) && enemy.CurrentBehaviour.GetType() == typeof(EnemyThunderBehaviour))
                    return false; 
            }
            
            return true;
        }

        IEnumerator ParalyzeImmunity(float _immunityTime)
        {
            canBeParalyzed = false;
            runningCorutineNumber++;
            yield return new WaitForSeconds(_immunityTime);
            runningCorutineNumber--;
            canBeParalyzed = true;
        }
    }
}