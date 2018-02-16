using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ElementalBehaviour : MonoBehaviour
    {
        ElementalEffectData elementalData;
        bool isInitialized;
        float timer;
        IBulletEffectBehaviour bulletBehaviour;

        // Update is called once per frame
        void Update()
        {
            if (!isInitialized)
                return;

            elementalData.TimeOfEffect -= Time.deltaTime;
            if(elementalData.TimeOfEffect <= timer)
            {
                bulletBehaviour.DoEffect();
                timer -= elementalData.TimeFraction;
            }

            if (elementalData.TimeOfEffect <= 0)
                StopEffect();
        }

        /// <summary>
        /// Set the time of effect with the float passed
        /// </summary>
        /// <param name="_timeOfEffect"></param>
        public void Init(IBulletEffectBehaviour _behaviour, Enemy _enemy, ElementalEffectData _elementalData)
        {
            bulletBehaviour = _behaviour;
            elementalData = _elementalData;
            bulletBehaviour.DoInit(_enemy, _elementalData.EffectDamage);
            if (CheckEffect(_behaviour, _enemy))
                isInitialized = true;
        }

        void StopEffect()
        {
            bulletBehaviour.DoStopEffect();
            isInitialized = false;
        }

        bool CheckEffect(IBulletEffectBehaviour _behaviour, Enemy _enemy)
        {
            if (_behaviour.GetType() == typeof(ElementalBehaviourFire) && _enemy.currentBehaviour.GetType() == typeof(FireBehaviour))
                return false;
            if (_behaviour.GetType() == typeof(ElementalBehaviourPoison) && _enemy.currentBehaviour.GetType() == typeof(PoisonBehaviour))
                return false;
            if (_behaviour.GetType() == typeof(ElementalBehaviourWater) && _enemy.currentBehaviour.GetType() == typeof(WaterBehaviour))
                return false;
            if (_behaviour.GetType() == typeof(ElementalBehaviourThunder) && _enemy.currentBehaviour.GetType() == typeof(ThunderBehaviour))
                return false;
            return true;
        }
    }
}