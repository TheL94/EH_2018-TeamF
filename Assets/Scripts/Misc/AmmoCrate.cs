using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class AmmoCrate : MonoBehaviour
    {
        public AmmoType Type { get; set; }
        public int Ammo;

        public void Init()
        {
            Type = (AmmoType)Random.Range(0, 4);
        }

        public void DestroyAmmoCrate()
        {
            gameObject.SetActive(false);
        }

    }
}