using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public interface IElementalEffectBehaviour
    {
        void DoInit(Enemy _enemy, ElementalEffectData _data);
        /// <summary>
        /// Funzione dove viene eseguito il comportamento specifico dell'elemento
        /// </summary>
        /// <returns>Ritorna true se il Timer all'interno dell'azione è terminato quindi deve essere chiamato lo stop dell'effetto</returns>
        bool DoUpdate();
        void DoStopEffect();
    }
}