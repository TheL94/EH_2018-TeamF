using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class AmmoCratesController : MonoBehaviour
    {

        AmmoCrate[] Crates;

        public void Init()
        {
            Crates = GetComponentsInChildren<AmmoCrate>();
            if (Crates.Length > 0)
            {
                foreach (AmmoCrate crate in Crates)
                {
                    crate.Init();
                } 
            }
        }

        public void ReinitAmmoCrates()
        {
            for (int i = 0; i < Crates.Length; i++)
            {
                Crates[i].gameObject.SetActive(true);
            }
        }
    }

    public enum AmmoType
    {
        Fire = 0,
        Water = 1,
        Poison = 2,
        Thunder = 3,
        None = 4
    }
}