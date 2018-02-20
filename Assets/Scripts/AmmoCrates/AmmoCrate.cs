using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class AmmoCrate : MonoBehaviour
    {
        AmmoCratesController controller;
        public ElementalType Type { get; set; }
        public int Ammo;

        public void Init(AmmoCratesController _controller)
        {
            controller = _controller;
            Type = (ElementalType)Random.Range(1, 5);
        }

        /// <summary>
        /// Chiama la funzioen nel controller per rimuoverla dalla lista di crate, distruggerla, 
        /// avviare la coroutine per istanziare una nuova cassa al suo posto.
        /// </summary>
        public void DestroyAmmoCrate()
        {
            controller.DeleteAmmoCrateFromList(this);
        }

    }
}