using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TeamF
{
    public class Character : MonoBehaviour, IDamageable, IParalyzable
    {
        public CharacterData Data { get; set; }
        public MeshRenderer BackPackRenderer;
        public MeshRenderer CharacterRenderer;
        [HideInInspector]
        public Movement movement;

        Player player;

        bool isInvincible;
        #region API
        public void Init(Player _player, CharacterData _data,  bool _isTestScene = false)
        {
            player = _player;
            Data = _data;

            Life = Data.Life;
            IsParalized = false;

            currentWeapon = GetComponentInChildren<Weapon>();
            movement = GetComponent<Movement>();

            movement.Init(Data.MovementSpeed, Data.RotationSpeed);

            List<BulletData> bulletDatasInstancies = new List<BulletData>();
            foreach (BulletData item in Data.BulletDatas)
                bulletDatasInstancies.Add(Instantiate(item));

            currentWeapon.Init(Data.BulletSpeed, Data.Ratio, Data.MagCapacity, bulletDatasInstancies);

            if (!_isTestScene)
                Data.AllElementalAmmo[0].Ammo = -1;
            else
            {
                isInvincible = _isTestScene;
                for (int i = 0; i < Data.AllElementalAmmo.Length; i++)
                {
                    Data.AllElementalAmmo[i].Ammo = -1;
                }
            }
            selectedAmmoIndex = 0;
        }
        #endregion

        #region IDamageable
        float _life;
        public float Life
        {
            get { return _life; }
            private set
            {
                _life = value;
                Events_UIController.LifeChanged(Life, Data.Life);
            }
        }

        public Vector3 Position { get { return transform.position; } }

        float _damagePercentage = 100;
        public float DamagePercentage
        {
            get { return _damagePercentage; }
            set { _damagePercentage = value; }
        }

        /// <summary>
        /// Provoca danno al player e cambia stato se la vita dell'avatar raggiunge lo zero.
        /// </summary>
        /// <param name="_damage">Valore da scalare alla vita dell'avatar</param>
        /// <param name="_type">Tipo del nemico che attacca, per triggherare azioni particolari del player a seconda del tipo di nemico</param>
        public void TakeDamage(float _damage, ElementalType _type = ElementalType.None)
        {
            if (isInvincible)
                return;
            _damage = (_damage * DamagePercentage) / 100;
            Life -= _damage;

            CharacterRenderer.material.DOColor(Color.white, .1f).OnComplete(() => { CharacterRenderer.material.DORewind(); });         

            if (Life <= 0)
            {
                //Destroy(movement.ModelToRotate);
                player.CharacterDeath();
            }
        }
        #endregion

        #region IParalyzable
        /// <summary>
        /// Chiamata dalla combo elementale paralizzante
        /// </summary>
        public bool IsParalized { get; set; }
        #endregion

        #region Weapon
        Weapon currentWeapon;

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
        #endregion

        #region Ammo
        int _selectedAmmoIndex;
        int selectedAmmoIndex
        {
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
            get { return Data.AllElementalAmmo[selectedAmmoIndex]; }
            set
            {
                Data.AllElementalAmmo[selectedAmmoIndex] = value;
                if (Data.AllElementalAmmo[selectedAmmoIndex].AmmoType != ElementalType.None)
                {
                    Events_UIController.AmmoChange(Data.AllElementalAmmo[selectedAmmoIndex]);
                }
            }
        }

        public void SelectPreviousAmmo()
        {
            selectedAmmoIndex++;
            if (selectedAmmoIndex > Data.AllElementalAmmo.Length - 1)
                selectedAmmoIndex = 0;
        }
        public void SelectNextAmmo()
        {
            selectedAmmoIndex--;
            if (selectedAmmoIndex < 0)
                selectedAmmoIndex = Data.AllElementalAmmo.Length - 1;
        }
        #endregion

        void PickupAmmo(AmmoCrate _crate)
        {
            if (_crate != null)
            {
                for (int i = 0; i < Data.AllElementalAmmo.Length; i++)
                {
                    if (_crate.Type == Data.AllElementalAmmo[i].AmmoType)
                    {
                        //Aggiungi le munizioni a questo tipo;
                        Data.AllElementalAmmo[i].Ammo += _crate.Ammo;
                        Events_UIController.AmmoChange(Data.AllElementalAmmo[i]);
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