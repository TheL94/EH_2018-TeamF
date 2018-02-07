using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class AvatarController : MonoBehaviour, IDamageable
    {

        Player player;
        public ElementalAmmo[] AllElementalAmmo = new ElementalAmmo[5];
        public int Life;
        [HideInInspector]
        public Movement movement;
        Weapon currentWeapon;

        ElementalAmmo _selectedAmmo;
        public ElementalAmmo SelectedAmmo
        {
            get { return _selectedAmmo; }
            set
            {
                _selectedAmmo = value;
                EventManager.AmmoChange(_selectedAmmo);
            }
        }

        public void Init(Player _player)
        {
            player = _player;
            currentWeapon = GetComponentInChildren<Weapon>();
            movement = GetComponent<Movement>();
            for (int i = 0; i < AllElementalAmmo.Length; i++)
            {
                if(i == AllElementalAmmo.Length - 1)
                    AllElementalAmmo[i] = new ElementalAmmo { AmmoType = (AmmoType)i, Ammo = -1 };
                else
                    AllElementalAmmo[i] = new ElementalAmmo { AmmoType = (AmmoType)i, Ammo = 0 };
            }
            SelectedAmmo = AllElementalAmmo[AllElementalAmmo.Length - 1];
        }

        /// <summary>
        /// Chiama la funzione di sparo nell'arma e sovrascrive la struttura appena passata
        /// </summary>
        public void Shot()
        {
            SelectedAmmo = currentWeapon.SingleShot(SelectedAmmo);
        }

        /// <summary>
        /// Chiama la funzione di sparo nell'arma e sovrascrive la struttura appena passata
        /// </summary>
        public void FullAutoShot()
        {
            SelectedAmmo = currentWeapon.FullAutoShoot(SelectedAmmo);
        }

        public void TakeDamage(int _damage)
        {
            Life -= _damage;
            if (Life <= 0)
            {
                //Destroy(movement.ModelToRotate);
                GameManager.I.ChangeFlowState(FlowState.EndGame);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            AmmoCrate crate = other.GetComponent<AmmoCrate>();
            if (crate != null)
            {
                for (int i = 0; i < AllElementalAmmo.Length; i++)
                {
                    if (crate.Type == AllElementalAmmo[i].AmmoType)
                    {
                        //Aggiungi le munizioni a questo tipo;
                        AllElementalAmmo[i].Ammo = crate.Ammo;
                        GameManager.I.UIMng.UI_GameplayCtrl.UpdateAmmo(AllElementalAmmo[i]);
                        crate.DestroyAmmoCrate();
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Setta le munizioni da utilizzare per sparare
        /// </summary>
        /// <param name="_type"></param>
        public void SetActiveAmmo(AmmoType _type)
        {
            switch (_type)
            {
                case AmmoType.Fire:
                    SelectedAmmo = AllElementalAmmo[0];
                    break;
                case AmmoType.Water:
                    SelectedAmmo = AllElementalAmmo[1];
                    break;
                case AmmoType.Poison:
                    SelectedAmmo = AllElementalAmmo[2];
                    break;
                case AmmoType.Thunder:
                    SelectedAmmo = AllElementalAmmo[3];
                    break;
                case AmmoType.None:
                    SelectedAmmo = AllElementalAmmo[4];
                    break;
            }
        }
    }


    public struct ElementalAmmo
    {
        public AmmoType AmmoType { get; set; }
        public int Ammo { get; set; }
    }
}