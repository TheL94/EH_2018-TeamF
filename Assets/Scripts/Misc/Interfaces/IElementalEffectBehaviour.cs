using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public interface IElementalEffectBehaviour
    {
        /// <summary>
        /// Inizializza l'effetto
        /// </summary>
        /// <param name="_target">Il riferimento a chi sta subendo l'effetto</param>
        /// <param name="_data">I dati dell'effetto: durata, valore, etc.</param>
        void DoInit(IEffectable _target, ElementalEffectData _data);
        /// <summary>
        /// Funzione dove viene eseguito il comportamento specifico dell'elemento
        /// </summary>
        /// <returns>Ritorna true se il Timer all'interno dell'azione è terminato quindi deve essere chiamato lo stop dell'effetto</returns>
        bool DoUpdate();
        void DoStopEffect();
    }
}