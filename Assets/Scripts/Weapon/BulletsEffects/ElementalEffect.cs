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

        // Update is called once per frame
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
            if (CheckEffect(_behaviour, _enemy))
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

        bool CheckEffect(IElementalEffectBehaviour _behaviour, Enemy _enemy)
        {
            if (_behaviour.GetType() == typeof(ElementalEffectFire) && _enemy.CurrentBehaviour.GetType() == typeof(EnemyFireBehaviour))
                return false;
            if (_behaviour.GetType() == typeof(ElementalEffectPoison) && _enemy.CurrentBehaviour.GetType() == typeof(EnemyPoisonBehaviour))
                return false;
            if (_behaviour.GetType() == typeof(ElementalEffectWater) && _enemy.CurrentBehaviour.GetType() == typeof(EnemyWaterBehaviour))
                return false;
            if (_behaviour.GetType() == typeof(ElementalEffectThunder) && _enemy.CurrentBehaviour.GetType() == typeof(EnemyThunderBehaviour))
                return false;
            return true;
        }
    }
}