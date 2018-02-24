using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ConfusionEffect : IElementalEffectBehaviour
    {
        Enemy enemy;
        ElementalEffectData elementalData;
        IDamageable myTarget;

        public void DoInit(Enemy _enemy, ElementalEffectData _data)
        {
            enemy = _enemy;
            elementalData = _data;
            myTarget = enemy.target;
            //enemy.ChangeMyTarget();
            Debug.Log(myTarget + " / " + enemy.target);
        }

        public void DoStopEffect()
        {
            //enemy.target = myTarget;
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
    }
}