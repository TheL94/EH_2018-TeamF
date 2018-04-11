using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TeamF
{
    public class Character : MonoBehaviour, IEffectable, IParalyzable
    {
        public CharacterData Data { get; set; }
        public MeshRenderer BackPackRenderer;
        public MeshRenderer CharacterRenderer;
        [HideInInspector]
        public Movement movement;

        #region IGetSlower
        public float MovementSpeed
        {
            get { return Data.MovementSpeed; }
            set
            {
                Data.MovementSpeed = value;
                movement.MovementSpeed = Data.MovementSpeed;
            }
        }
        #endregion

        Player player;

        bool isInvincible;
        #region API
        public void Init(Player _player, CharacterData _data,  bool _isTestScene = false)
        {
            player = _player;
            Data = _data;

            Life = Data.Life;
            IsParalized = false;

            weaponController = GetComponentInChildren<WeaponController>();
            movement = GetComponent<Movement>();

            movement.Init(MovementSpeed, Data.RotationSpeed, Data.DashValues);

            List<BulletData> bulletDatasInstancies = new List<BulletData>();
            foreach (BulletData item in Data.BulletDatas)
                bulletDatasInstancies.Add(Instantiate(item));

            weaponController.Init(bulletDatasInstancies);

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
            selectedAmmoIndex = 1;
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
        public WeaponController weaponController { get; private set; }

        public void DefaultShot()
        {
            weaponController.Shot(Data.AllElementalAmmo[0]);
        }

        /// <summary>
        /// Chiama la funzione di sparo nell'arma e sovrascrive la struttura appena passata
        /// </summary>
        public void ElementalShot()
        {
            weaponController.Shot(SelectedAmmo);
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
                switch (_selectedAmmoIndex)         // Colora lo zaino del character
                {
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
                        BackPackRenderer.material.color = Color.yellow;
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
            set { Data.AllElementalAmmo[selectedAmmoIndex] = value; }
        }

        

        public void SelectPreviousAmmo()
        {
            selectedAmmoIndex++;
            if (selectedAmmoIndex > Data.AllElementalAmmo.Length - 1)
                selectedAmmoIndex = 1;
        }
        public void SelectNextAmmo()
        {
            selectedAmmoIndex--;
            if (selectedAmmoIndex < 1)
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

        void PickupWeapon(WeaponCrate _crate)
        {
            if(_crate != null)
            {
                weaponController.SetCurrentWeapon(_crate.WeaponType);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            PickupAmmo(other.GetComponent<AmmoCrate>());
            PickupWeapon(other.GetComponent<WeaponCrate>());
        }
    }

    [System.Serializable]
    public class ElementalAmmo
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