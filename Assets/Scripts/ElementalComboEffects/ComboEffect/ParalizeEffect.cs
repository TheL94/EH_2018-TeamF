using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ParalizeEffect : IElementalEffectBehaviour
    {
        IParalyzable paralyzable;
        ElementalEffectData elementalData;

        public void DoInit(Enemy _enemy, ElementalEffectData _data)
        {
            paralyzable = _enemy;
            elementalData = _data;
            paralyzable.IsParalized = true;
        }

        public void DoStopEffect()
        {
            paralyzable.IsParalized = false; //TODO: alla fine dell'effetto il nemico ne deve essere immune per 2 secondi
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