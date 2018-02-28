using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class Events_LevelController 
    {
        #region Update KillPoints Counter
        /// <summary>
        /// Evento per aggiornare i punti uccisione nel level manager
        /// </summary>
        public delegate void LevelKillPointsUpdate(float _killedEnemyValue);

        public static event LevelKillPointsUpdate OnKillPointChanged;

        public static void UpdateKillPoints(float _killedEnemyValue)
        {
            if (OnKillPointChanged != null)
                OnKillPointChanged(_killedEnemyValue);
        }

        #endregion
    }
}
