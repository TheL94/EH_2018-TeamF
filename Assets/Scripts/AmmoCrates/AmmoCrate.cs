using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class AmmoCrate : MonoBehaviour
    {
        public ElementalType Type { get; set; }

        [HideInInspector]
        public GameObject Graphic;
        [HideInInspector]
        public string CurrentGraphicID;

        [HideInInspector]
        public int Ammo;
        AmmoCratesController controller;
        Color ammoColor;


        public void Init(AmmoCratesController _controller, int _ammo)
        {
            controller = _controller;
            Ammo = _ammo;
            Type = (ElementalType)Random.Range(1, 5);
            switch (Type)
            {
                case ElementalType.None:
                    break;
                case ElementalType.Fire:
                    CurrentGraphicID = "FireIcon";
                    SetGraphic(GameManager.I.PoolMng.GetObject(CurrentGraphicID));
                    ammoColor = Color.red;
                    break;
                case ElementalType.Water:
                    CurrentGraphicID = "WaterIcon";
                    SetGraphic(GameManager.I.PoolMng.GetObject(CurrentGraphicID));
                    ammoColor = Color.blue;
                    break;
                case ElementalType.Poison:
                    CurrentGraphicID = "PoisonIcon";
                    SetGraphic(GameManager.I.PoolMng.GetObject(CurrentGraphicID));
                    ammoColor = Color.green;
                    break;
                case ElementalType.Thunder:
                    CurrentGraphicID = "ThunderIcon";
                    SetGraphic(GameManager.I.PoolMng.GetObject(CurrentGraphicID));
                    ammoColor = Color.yellow;
                    break;
            }
        }

        /// <summary>
        /// Chiama la funzione nel controller per rimuoverla dalla lista di crate, distruggerla, 
        /// avviare la coroutine per istanziare una nuova cassa al suo posto.
        /// </summary>
        public void DestroyAmmoCrate()
        {
            Graphic.SetActive(false);
            GameManager.I.PoolMng.UpdatePool(CurrentGraphicID);
            // Crea la particles per indicare quante ammo sono state raccolte
            GetComponent<HPScript>().ChangeHP(transform.position + new Vector3(0,5,0), ammoColor, Type.ToString() + " " + Ammo);
            controller.DeleteAmmoCrateFromList(this);
        }

        void SetGraphic(GameObject _pool)
        {
            Graphic = _pool;
            Graphic.transform.position = transform.position;
            Graphic.transform.rotation = transform.rotation;
            Graphic.transform.parent = transform;
        }
    }
}