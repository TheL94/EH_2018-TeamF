using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ParalizeEffect : IElementalEffectBehaviour
    {
        Enemy enemy;
        ElementalEffectData elementalData;

        public void DoInit(Enemy _enemy, ElementalEffectData _data)
        {
            enemy = _enemy;
            elementalData = _data;
            enemy.GetComponent<IParalyzable>().IsParalized = true;
        }

        public void DoStopEffect()
        {
            enemy.GetComponent<IParalyzable>().IsParalized = false; //TODO: alla fine dell'effetto il nemico ne deve essere immune per 2 secondi
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