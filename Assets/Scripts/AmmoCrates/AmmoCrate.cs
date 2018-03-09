using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class AmmoCrate : MonoBehaviour
    {
        public ElementalType Type { get; set; }

        [HideInInspector]
        public int Ammo;
        AmmoCratesController controller;
        Color ammoColor;
        string ammoName;

        public void Init(AmmoCratesController _controller, int _ammo)
        {
            controller = _controller;
            Ammo = _ammo;
            Type = (ElementalType)Random.Range(1, 5);
            MeshRenderer render = GetComponent<MeshRenderer>();
            switch (Type)
            {
                case ElementalType.None:
                    break;
                case ElementalType.Fire:
                    ammoColor = render.material.color = Color.red;
                    ammoName = "Fire ";
                    break;
                case ElementalType.Water:
                    ammoColor = render.material.color = Color.blue;
                    ammoName = "Water ";
                    break;
                case ElementalType.Poison:
                    ammoColor = render.material.color = Color.green;
                    ammoName = "Poison ";
                    break;
                case ElementalType.Thunder:
                    ammoColor = render.material.color = Color.yellow;
                    ammoName = "Thunder ";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Chiama la funzioen nel controller per rimuoverla dalla lista di crate, distruggerla, 
        /// avviare la coroutine per istanziare una nuova cassa al suo posto.
        /// </summary>
        public void DestroyAmmoCrate()
        {
            // Crea la particles per indicare quante ammo sono state raccolte
            GetComponent<HPScript>().ChangeHP(transform.position + new Vector3(0,5,0), ammoColor, ammoName + Ammo);
            controller.DeleteAmmoCrateFromList(this);
        }
    }
}