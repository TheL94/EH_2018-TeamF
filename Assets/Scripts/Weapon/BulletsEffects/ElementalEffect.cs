using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ElementalEffect : MonoBehaviour
    {
        ElementalEffectData elementalData;
        bool isInitialized;
        float timer;
        IElementalEffectBehaviour bulletBehaviour;

        void Update()
        {
            if (!isInitialized)
                return;

            //bulletBehaviour.DoUpdate();
            if (bulletBehaviour.DoUpdate())
                StopEffect();
        }

        /// <summary>
        /// Set the time of effect with the float passed
        /// </summary>
        /// <param name="_timeOfEffect"></param>
        public void Init(IElementalEffectBehaviour _behaviour, Enemy _enemy, ElementalEffectData _elementalData)
        {
            bulletBehaviour = _behaviour;
            elementalData = _elementalData;
            if (CheckIfEffectCanBeApplied(_behaviour, _enemy.CurrentBehaviour))
            {
                bulletBehaviour.DoInit(_enemy, _elementalData);
                isInitialized = true;
            }
        }

        void StopEffect()
        {
            bulletBehaviour.DoStopEffect();
            isInitialized = false;
        }

        /// <summary>
        /// Funzione che ritorna true se l'effetto elementale può essere applicato per il tipo di nemico, altrimenti ritorna false
        /// </summary>
        /// <param name="_behaviour"></param>
        /// <param name="_enemy"></param>
        /// <returns></returns>
        bool CheckIfEffectCanBeApplied(IElementalEffectBehaviour _behaviour, IEnemyBehaviour _enemyBehaviour)
        {
            if (_behaviour.GetType() == typeof(ElementalEffectFire) && _enemyBehaviour.GetType() == typeof(EnemyFireBehaviour))
                return false;
            if (_behaviour.GetType() == typeof(ElementalEffectPoison) && _enemyBehaviour.GetType() == typeof(EnemyPoisonBehaviour))
                return false;
            if (_behaviour.GetType() == typeof(ElementalEffectWater) && _enemyBehaviour.GetType() == typeof(EnemyWaterBehaviour))
                return false;
            if (_behaviour.GetType() == typeof(ElementalEffectThunder) && _enemyBehaviour.GetType() == typeof(EnemyThunderBehaviour))
                return false;
            return true;
        }
    }
}