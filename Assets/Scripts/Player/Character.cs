using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class Character : MonoBehaviour, IDamageable
    {
        Player player;
        public ElementalAmmo[] AllElementalAmmo = new ElementalAmmo[5];
        public float Life;
        [HideInInspector]
        public Movement movement;
        Weapon currentWeapon;

        int selectedAmmoIndex;
        public ElementalAmmo SelectedAmmo
        {
            get { return AllElementalAmmo[selectedAmmoIndex]; }
            set
            {
                AllElementalAmmo[selectedAmmoIndex] = value;
                if (AllElementalAmmo[selectedAmmoIndex].AmmoType != ElementalType.None)
                {
                    EventManager.AmmoChange(AllElementalAmmo[selectedAmmoIndex]); 
                }
            }
        }

        #region API
        public void Init(Player _player)
        {
            player = _player;
            currentWeapon = GetComponentInChildren<Weapon>();
            movement = GetComponent<Movement>();
            AllElementalAmmo[AllElementalAmmo.Length - 1].Ammo = -1;
            
            selectedAmmoIndex = AllElementalAmmo.Length - 1;     
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

        /// <summary>
        /// Provoca danno al player e cambia stato se la vita dell'avatar raggiunge lo zero.
        /// </summary>
        /// <param name="_damage">Valore da scalare alla vita dell'avatar</param>
        /// <param name="_type">Tipo del nemico che attacca, per triggherare azioni particolari del player a seconda del tipo di nemico</param>
        public void TakeDamage(float _damage, ElementalType _type = ElementalType.None)
        {
            Life -= _damage;
            if (Life <= 0)
            {
                //Destroy(movement.ModelToRotate);
                player.AvatarDeath();
            }
        }

        /// <summary>
        /// Setta le munizioni da utilizzare per sparare
        /// </summary>
        /// <param name="_type"></param>
        public void SetActiveAmmo(ElementalType _type)
        {
            switch (_type)
            {
                case ElementalType.Fire:
                    selectedAmmoIndex = 0;
                    break;
                case ElementalType.Water:
                    selectedAmmoIndex = 1;
                    break;
                case ElementalType.Poison:
                    selectedAmmoIndex = 2;
                    break;
                case ElementalType.Thunder:
                    selectedAmmoIndex = 3;
                    break;
                case ElementalType.None:
                    selectedAmmoIndex = 4;
                    break;
            }
        }
        #endregion

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
                        AllElementalAmmo[i].Ammo += crate.Ammo;
                        EventManager.AmmoChange(AllElementalAmmo[i]);
                        crate.DestroyAmmoCrate();
                        return;
                    }
                }
            }
        }
    }
    [System.Serializable]
    public struct ElementalAmmo
    {
        public ElementalType AmmoType;
        public float Damage;
        public int Ammo;
    }
}