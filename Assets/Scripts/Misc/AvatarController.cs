using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class AvatarController : MonoBehaviour, IDamageable
    {

        Player player;
        public ElementalAmmo AmmoInventory;
        public int Life;
        [HideInInspector]
        public Movement movement;
        Weapon currentWeapon;

        public void Init(Player _player)
        {
            player = _player;
            currentWeapon = GetComponentInChildren<Weapon>();
            movement = GetComponent<Movement>();
            AmmoInventory = new ElementalAmmo();
        }

        /// <summary>
        /// Call the function single shot in the current weapon
        /// </summary>
        public void Shot()
        {
            currentWeapon.SingleShot();
        }

        /// <summary>
        /// Call the function shot full auto in the current weapon
        /// </summary>
        public void FullAutoShot()
        {
            currentWeapon.FullAutoShoot();
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
            Debug.Log("Collision");
            AmmoCrate crate = other.GetComponent<AmmoCrate>();
            if (crate != null)
            {
                switch (crate.Type)
                {
                    case AmmoType.Fire:
                        AmmoInventory.FireAmmo += crate.Ammo;
                        break;
                    case AmmoType.Water:
                        AmmoInventory.WaterAmmo += crate.Ammo;
                        break;
                    case AmmoType.Poison:
                        AmmoInventory.PoisonAmmo += crate.Ammo;
                        break;
                    case AmmoType.Thunder:
                        AmmoInventory.ThunderAmmo += crate.Ammo;
                        break;
                }
                GameManager.I.UIMng.UI_GameplayCtrl.UpdateAmmo(AmmoInventory, crate.Type);
                crate.DestroyAmmoCrate();
            }
        }

    }
}

public struct ElementalAmmo
{
    public int FireAmmo;
    public int WaterAmmo;
    public int PoisonAmmo;
    public int ThunderAmmo;
}