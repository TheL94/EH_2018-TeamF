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
        [HideInInspector]
        public Movement movement;

        List<BulletData> bulletDatasInstancies = new List<BulletData>();

        Player player;
        FadeToMe FadeComponent;
        BlinkController blinkCtrl;

        bool isInvincible;
        #region API
        public void Init(Player _player, CharacterData _data,  bool _isTestScene = false)
        {
            player = _player;
            Data = _data;

            MovementSpeed = Data.MovementSpeed;
            movement = GetComponent<Movement>();
            movement.Init(this, Data.DashValues);

            Life = Data.Life;
            IsParalyzed = false;

            FadeComponent = GetComponentInChildren<FadeToMe>();
            FadeComponent.Init();

            weaponController = GetComponentInChildren<WeaponController>();
            blinkCtrl = GetComponentInChildren<BlinkController>();

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

        public void ReInit()
        {
            GetComponentInChildren<ParticlesController>().StopAllParticles();
            blinkCtrl.ResetEffects();
            GetComponentInChildren<EffectController>().StopAllEffects();

            bulletDatasInstancies.Clear();
            MovementSpeed = Data.MovementSpeed;

            Life = Data.Life;
            Events_UIController.LifeChanged(Life, Data.Life);
        }

        public void StopWalkAnimation()
        {
            Animator anim = GetComponentInChildren<Animator>();
            anim.SetFloat("Forward", 0f);
            anim.SetFloat("Turn", 0f);
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
            if (isInvincible || Life <= 0 || GameManager.I.CurrentState != FlowState.Gameplay)
                return;

            _damage = (_damage * DamagePercentage) / 100;
            Life -= _damage;
            GameManager.I.AudioMng.PlaySound(Clips.CharacterDamage);

            if (ScoreCounter.OnScoreAction != null)
                ScoreCounter.OnScoreAction(ScoreType.PlayerDamage, transform.position);

            if (blinkCtrl != null)
                blinkCtrl.DamageBlink();

            if (Life <= 0)
            {
                player.CharacterDeath();
            }
        }
        #endregion

        #region IGetSlower
        public bool IsSlowed { get; set; }

        float _movementSpeed;
        public float MovementSpeed
        {
            get { return _movementSpeed; }
            set { _movementSpeed = value; }
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
                        BackPackLight.color = Color.green;
                        BackPackRenderer.materials[1].SetColor("_EmissionColor", Color.green);                   
                        break;
                    case 3:
                        BackPackLight.color = Color.blue;
                        BackPackRenderer.materials[1].SetColor("_EmissionColor", Color.blue);
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
                OnSelectedAmmoChanged();
            }
        }

        public BulletData SelectedAmmo
        {
            get { return bulletDatasInstancies[selectedAmmoIndex]; }
            set { bulletDatasInstancies[selectedAmmoIndex] = value; }
        }

        void OnSelectedAmmoChanged()
        {
            GameManager.I.UIMng.UI_GameplayCtrl.UpdateSelectedAmmo(SelectedAmmo.ElementalAmmo);
        }

        public void SelectSpecificAmmo(int _index)
        {
            selectedAmmoIndex = _index;
        }

        public void SelectPreviousAmmo()
        {
            if (selectedAmmoIndex + 1 > Data.BulletDatas.Length - 1)
                selectedAmmoIndex = 1;
            else
                selectedAmmoIndex++;

        }
        public void SelectNextAmmo()
        {
            if (selectedAmmoIndex - 1 < 1)
                selectedAmmoIndex = Data.BulletDatas.Length - 1;
            else
                selectedAmmoIndex--;
        }
        #endregion

        void PickupAmmo(AmmoCrate _crate)
        {
            if (_crate != null)
            {
                GameManager.I.AudioMng.PlaySound(Clips.CharacterPickUp);
                for (int i = 0; i < Data.BulletDatas.Length; i++)
                {
                    if (_crate != null && _crate.Type == Data.BulletDatas[i].ElementalAmmo.AmmoType)
                    {
                        //Aggiungi le munizioni a questo tipo;
                        if ((bulletDatasInstancies[i].ElementalAmmo.Ammo + _crate.Ammo) > bulletDatasInstancies[i].TotalAmmo)
                            bulletDatasInstancies[i].ElementalAmmo.Ammo = bulletDatasInstancies[i].TotalAmmo;
                        else
                            bulletDatasInstancies[i].ElementalAmmo.Ammo += _crate.Ammo;

                        Events_UIController.AmmoChange(bulletDatasInstancies[i].ElementalAmmo);
                        _crate.CrateCollected();
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
}