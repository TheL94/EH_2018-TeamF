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

        public delegate void UIAction(ElementalAmmo _elementalAmmo);

        public static event UIAction OnAmmoChanged;
    }
}