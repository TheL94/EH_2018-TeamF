using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF {
    public class BulletEffectWater : IElementalEffectBehaviour {

        float initialSlowdown;
        Enemy enemy;
        ElementalEffectData elementalData;

        public void DoInit(Enemy _enemy, ElementalEffectData _data)
        {
            enemy = _enemy;
            elementalData = _data;
            initialSlowdown = enemy.MovementSpeed;
            enemy.MovementSpeed -= _data.EffectValue;
        }

        public bool DoUpdate()
        {
            elementalData.TimeOfEffect -= Time.deltaTime;
            if (elementalData.TimeOfEffect <= 0)
            {
                return true;
            }
            return false;
        }

        public void DoStopEffect()
        {
            enemy.MovementSpeed = initialSlowdown;
        }
    }
}