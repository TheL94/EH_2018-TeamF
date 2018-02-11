using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EventManager
    {
        /// <summary>
        /// Chiama l'evento per aggiornare le munizioni in UI
        /// </summary>
        /// <param name="_elementalAmmo"></param>
        public static void AmmoChange(ElementalAmmo _elementalAmmo)
        {
            if (OnAmmoChanged != null)
            {
                OnAmmoChanged(_elementalAmmo); 
            }
        }

        public delegate void UIAmmoAction(ElementalAmmo _elementalAmmo);

        public static event UIAmmoAction OnAmmoChanged;

        public delegate void UIKillPointsUpdate(float _currentPoints, float _pointsToWin);

        public static event UIKillPointsUpdate OnPointsChanged;

        public static void KillPointsChanged(float _currentPoints, float _pointsToWin)
        {
            if (OnPointsChanged != null)
                OnPointsChanged(_currentPoints, _pointsToWin);
        }
    }
}