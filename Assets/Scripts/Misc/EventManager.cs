using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EventManager
    {
        #region Update Ammo Event
        public delegate void UIAmmoAction(ElementalAmmo _elementalAmmo);

        public static event UIAmmoAction OnAmmoChanged;
        
        /// <summary>
        /// Chiama l'evento per aggiornare le munizioni in UI
        /// </summary>
        /// <param name="_elementalAmmo"></param>
        public static void AmmoChange(ElementalAmmo _elementalAmmo)
        {
            if (OnAmmoChanged != null)
                OnAmmoChanged(_elementalAmmo); 
        }

        #endregion

        #region KillPoints Event
        public delegate void UIKillPointsUpdate(float _currentPoints, float _pointsToWin);

        public static event UIKillPointsUpdate OnPointsChanged;

        public static void KillPointsChanged(float _currentPoints, float _pointsToWin)
        {
            if (OnPointsChanged != null)
                OnPointsChanged(_currentPoints, _pointsToWin);
        }
        #endregion

        #region Update Life Slider Event
        public delegate void UIUpdateLife(float _life, float _totalLife);

        public static UIUpdateLife OnLifeChanged;

        public static void LifeChanged(float _life, float _totalLife)
        {
            if (OnLifeChanged != null)
                OnLifeChanged(_life, _totalLife); 
        }
        #endregion
    }
}