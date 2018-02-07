using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EventManager
    {

        public static void AmmoChange(ElementalAmmo _elementalAmmo)
        {
            OnAmmoChanged(_elementalAmmo);
        }

        public delegate void UIAction(ElementalAmmo _elementalAmmo);

        public static event UIAction OnAmmoChanged;
    }
}