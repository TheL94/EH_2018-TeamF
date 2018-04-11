using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EffectController : MonoBehaviour
    {
        bool isBulletBehaviourInitialized;
        bool isComboInitialized;

        IElementalEffectBehaviour bulletBehaviour;
        IElementalEffectBehaviour comboBehaviour;

        void Update()
        {
            if (isBulletBehaviourInitialized)
            {
                //bulletBehaviour.DoUpdate();
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
                bulletBehaviour = _behaviour;
                if (CheckIfEffectCanBeApplied(_behaviour, _target))
                {
                    bulletBehaviour.DoInit(_target, _elementalData);
                    isBulletBehaviourInitialized = true;
                } 
            }
            else
            {
                comboBehaviour = _behaviour;
                comboBehaviour.DoInit(_target, _elementalData);
                isComboInitialized = true;
            }
        }

        void StopBulletEffect()
        {
            bulletBehaviour.DoStopEffect();
            isBulletBehaviourInitialized = false;
        }

        void StopComboEffect()
        {
            comboBehaviour.DoStopEffect();
            isComboInitialized = false;
        }

        /// <summary>
        /// Funzione che ritorna true se l'effetto elementale può essere applicato per il tipo di nemico, altrimenti ritorna false
        /// </summary>
        /// <param name="_behaviour"></param>
        /// <param name="_enemy"></param>
        /// <returns></returns>
        bool CheckIfEffectCanBeApplied(IElementalEffectBehaviour _behaviour, IEffectable _enemyBehaviour)
        {
            Enemy enemy = _enemyBehaviour as Enemy;

            if (enemy != null)
            {
                if (_behaviour.GetType() == typeof(BulletEffectFire) && enemy.CurrentBehaviour.GetType() == typeof(EnemyFireBehaviour))
                    return false;
                if (_behaviour.GetType() == typeof(BulletEffectPoison) && enemy.CurrentBehaviour.GetType() == typeof(EnemyPoisonBehaviour))
                    return false;
                if (_behaviour.GetType() == typeof(BulletEffectWater) && enemy.CurrentBehaviour.GetType() == typeof(EnemyWaterBehaviour))
                    return false;
                if (_behaviour.GetType() == typeof(BulletEffectThunder) && enemy.CurrentBehaviour.GetType() == typeof(EnemyThunderBehaviour))
                    return false; 
            }
            return true;
        }
    }
}