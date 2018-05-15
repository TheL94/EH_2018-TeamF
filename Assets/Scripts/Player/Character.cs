using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TeamF
{
    public class Character : MonoBehaviour, IEffectable, IShooter
    {
        public CharacterData Data { get; private set; }
        public Light BackPackLight;
        public SkinnedMeshRenderer BackPackRenderer;
        public SkinnedMeshRenderer CharacterRenderer;
        [HideInInspector]
        public Movement movement;

        List<BulletData> bulletDatasInstancies = new List<BulletData>();

        Player player;

        FadeToMe FadeComponent;

        bool isInvincible;
        #region API
        public void Init(Player _player, CharacterData _data,  bool _isTestScene = false)
        {
            player = _player;
            Data = _data;

            movement = GetComponent<Movement>();
            movement.Init(MovementSpeed, Data.RotationSpeed, Data.DashValues);

            Life = Data.Life;
            IsParalyzed = false;

            FadeComponent = GetComponentInChildren<FadeToMe>();
            FadeComponent.Init();
            weaponController = GetComponentInChildren<WeaponController>();



            if (bulletDatasInstancies.Count == 0)
            {
                foreach (BulletData item in Data.BulletDatas)
                    bulletDatasInstancies.Add(Instantiate(item)); 
            }

            weaponController.Init(bulletDatasInstancies, this);

            if (!_isTestScene)
                bulletDatasInstancies[0].ElementalAmmo.Ammo = -1;
            else
            {
                isInvincible = _isTestScene;
                for (int i = 0; i < bulletDatasInstancies.Count; i++)
                {
                    bulletDatasInstancies[i].ElementalAmmo.Ammo = -1;
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
                player.CharacterDeath();
            }
        }
        #endregion

        #region IGetSlower
        public bool IsSlowed { get; set; }

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

        #region IParalyzable
        /// <summary>
        /// Chiamata dalla combo elementale paralizzante
        /// </summary>
        public bool IsParalyzed { get; set; }
        #endregion

        #region Weapon
        public WeaponController weaponController { get; private set; }

        public void DefaultShot()
        {
            weaponController.Shot(bulletDatasInstancies[0]);
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
                        BackPackLight.color = Color.red;
                        BackPackRenderer.materials[1].SetColor("_EmissionColor", Color.red);
                        break;
                    case 2:
                        BackPackLight.color = Color.blue;
                        BackPackRenderer.materials[1].SetColor("_EmissionColor", Color.blue);
                        break;
                    case 3:
                        BackPackLight.color = Color.green;
                        BackPackRenderer.materials[1].SetColor("_EmissionColor", Color.green);
                        break;
                    case 4:
                        BackPackLight.color = Color.yellow;
                        BackPackRenderer.materials[1].SetColor("_EmissionColor", Color.yellow);
                        break;
                    default:
                        BackPackLight.color = Color.white;
                        BackPackRenderer.materials[1].SetColor("_EmissionColor", Color.white);
                        break;
                }
            }
        }

        public BulletData SelectedAmmo
        {
            get { return bulletDatasInstancies[selectedAmmoIndex]; }
            set { bulletDatasInstancies[selectedAmmoIndex] = value; }
        }

        

        public void SelectPreviousAmmo()
        {
            selectedAmmoIndex++;
            if (selectedAmmoIndex > Data.BulletDatas.Length - 1)
                selectedAmmoIndex = 1;

        }
        public void SelectNextAmmo()
        {
            selectedAmmoIndex--;
            if (selectedAmmoIndex < 1)
                selectedAmmoIndex = Data.BulletDatas.Length - 1;
        }
        #endregion

        void PickupAmmo(AmmoCrate _crate)
        {
            if (_crate != null)
            {
                for (int i = 0; i < Data.BulletDatas.Length; i++)
                {
                    if (_crate.Type == Data.BulletDatas[i].ElementalAmmo.AmmoType)
                    {
                        //Aggiungi le munizioni a questo tipo;
                        bulletDatasInstancies[i].ElementalAmmo.Ammo += _crate.Ammo;
                        Events_UIController.AmmoChange(bulletDatasInstancies[i].ElementalAmmo);
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

    

}