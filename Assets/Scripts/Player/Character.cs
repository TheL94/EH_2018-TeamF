using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TeamF
{
    public class Character : MonoBehaviour, IDamageable, IParalyzable
    {
        Player player;
        public CharacterData CharacterData;
        CharacterData data;
        public float Life
        {
            get { return data.Life; }
            set
            {
                data.Life = value;
                CharacterRenderer.material.DOColor(Color.white, .1f).OnComplete(() => { CharacterRenderer.material.DORewind(); });
                EventManager.LifeChanged(data.Life, CharacterData.Life);
            }
        }

        public Vector3 Position
        {
            get { return transform.position; }
            set { transform.position = value; }
        }
        public MeshRenderer BackPackRenderer;
        public MeshRenderer CharacterRenderer;
        [HideInInspector]
        public Movement movement;
        Weapon currentWeapon;

        int _selectedAmmoIndex;
        int selectedAmmoIndex {
            get { return _selectedAmmoIndex; }
            set
            {
                _selectedAmmoIndex = value;
                switch (_selectedAmmoIndex)
                {
                    case 0:
                        BackPackRenderer.material.color = Color.grey;
                        break;
                    case 1:
                        BackPackRenderer.material.color = Color.red;
                        break;
                    case 2:
                        BackPackRenderer.material.color = Color.blue;
                        break;
                    case 3:
                        BackPackRenderer.material.color = Color.green;
                        break;
                    case 4:
                        BackPackRenderer.material.color = Color.magenta;
                        break;
                    default:
                        BackPackRenderer.material.color = Color.grey;
                        break;
                }
            }
        }
        public ElementalAmmo SelectedAmmo
        {
            get { return data.AllElementalAmmo[selectedAmmoIndex]; }
            set
            {
                data.AllElementalAmmo[selectedAmmoIndex] = value;
                if (data.AllElementalAmmo[selectedAmmoIndex].AmmoType != ElementalType.None)
                {
                    EventManager.AmmoChange(data.AllElementalAmmo[selectedAmmoIndex]); 
                }
            }
        }

        Color startColor;


        #region API
        public void Init(Player _player)
        {
            player = _player;
            currentWeapon = GetComponentInChildren<Weapon>();
            movement = GetComponent<Movement>();
            InitOfTheData();
            data.AllElementalAmmo[0].Ammo = -1;
            selectedAmmoIndex = 0;
            startColor = CharacterRenderer.material.color;

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
        public float DamagePercentage { get; set; }

        /// <summary>
        /// Provoca danno al player e cambia stato se la vita dell'avatar raggiunge lo zero.
        /// </summary>
        /// <param name="_damage">Valore da scalare alla vita dell'avatar</param>
        /// <param name="_type">Tipo del nemico che attacca, per triggherare azioni particolari del player a seconda del tipo di nemico</param>
        public void TakeDamage(float _damage, ElementalType _type = ElementalType.None)
        {
            _damage += _damage * DamagePercentage;
            Life -= _damage;
            if (Life <= 0)
            {
                //Destroy(movement.ModelToRotate);
                player.AvatarDeath();
            }
        }
        #endregion

        public void SelectPreviousAmmo()
        {
            selectedAmmoIndex++;
            if (selectedAmmoIndex > data.AllElementalAmmo.Length - 1)
                selectedAmmoIndex = 0;
        }
        public void SelectNextAmmo()
        {
            selectedAmmoIndex--;
            if (selectedAmmoIndex < 0)
                selectedAmmoIndex = data.AllElementalAmmo.Length - 1;
        }
        #region IParalyzable
        /// <summary>
        /// Setta la variabile booleana isParalized nel player
        /// </summary>
        /// <param name="_isParalized">Valore da assegnare alla variabile nel player; True: vengono bloccati i movimenti; False: vengono attivati</param>
        public void Paralize(bool _isParalized)
        {
            player.IsParalized = _isParalized;
        }

        #endregion
        #endregion

        void InitOfTheData()
        {
            data = Instantiate(CharacterData);
            movement.Init(data.MovementSpeed, data.RotationSpeed);
            currentWeapon.Init(data.BulletSpeed, data.Ratio, data.MagCapacity, data.BulletPrefab);
        }

        void PickupAmmo(AmmoCrate _crate)
        {
            if (_crate != null)
            {
                for (int i = 0; i < data.AllElementalAmmo.Length; i++)
                {
                    if (_crate.Type == data.AllElementalAmmo[i].AmmoType)
                    {
                        //Aggiungi le munizioni a questo tipo;
                        data.AllElementalAmmo[i].Ammo += _crate.Ammo;
                        EventManager.AmmoChange(data.AllElementalAmmo[i]);
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