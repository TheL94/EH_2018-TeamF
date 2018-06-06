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

    public static class IElementalEffectBehaviourExtension
    {
        /// <summary>
        /// Instanzia il prefab della combo elementale e lancia l'evento per in counter delle combo elementali.
        /// </summary>
        /// <param name="_folderPath">Il percorso all'interno della cartella resources del prefab della combo elementale</param>
        public static void AddScore(this IElementalEffectBehaviour _EffectBehaviour, IEffectable _target)
        {
            if (_target.GetType().IsAssignableFrom(typeof(Enemy)))
                if (ScoreCounter.OnScoreAction != null)
                    ScoreCounter.OnScoreAction(ScoreType.EnemyComboEffet);
        }
    }   
}