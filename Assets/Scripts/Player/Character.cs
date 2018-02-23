using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class Character : MonoBehaviour, IDamageable, IParalyzable
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
            AllElementalAmmo[0].Ammo = -1;
            
            selectedAmmoIndex = 0;     
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

        #region IDamageable
        public float DamageMultiplier { get; set; }

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
        #endregion

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

        #region Interface IParalyzable
        /// <summary>
        /// Setta la variabile booleana isParalized nel player
        /// </summary>
        /// <param name="_isParalized">Valore da assegnare alla variabile nel player; True: vengono bloccati i movimenti; False: vengono attivati</param>
        public void Paralize(float _timeOfParalysis)
        {
            player.IsParalized = true;
            StartCoroutine(DisableParalysis(_timeOfParalysis));
        }

        IEnumerator DisableParalysis(float _secodns)
        {
            yield return new WaitForSeconds(_secodns);
            player.IsParalized = false;
        }
        #endregion
        #endregion

        void PickupAmmo(AmmoCrate _crate)
        {
            if (_crate != null)
            {
                for (int i = 0; i < AllElementalAmmo.Length; i++)
                {
                    if (_crate.Type == AllElementalAmmo[i].AmmoType)
                    {
                        //Aggiungi le munizioni a questo tipo;
                        AllElementalAmmo[i].Ammo += _crate.Ammo;
                        EventManager.AmmoChange(AllElementalAmmo[i]);
                        _crate.DestroyAmmoCrate();
                        return;
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            PickupAmmo(other.GetComponent<AmmoCrate>());
        }
    }

    [System.Serializable]
    public struct ElementalAmmo
    {
        public ElementalType AmmoType;
        public float Damage;
        public int Ammo;
        public ElementalEffectData Data;
        
    }

    [System.Serializable]
    public struct ElementalEffectData
    {
        public float EffectValue;
        public float TimeOfEffect;
        public float TimeFraction;
    }
}