using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class AvatarController : MonoBehaviour, IDamageable
    {

        Player player;
        public int Life;
        [HideInInspector]
        public Movement movement;
        Weapon currentWeapon;

        public void Init(Player _player)
        {
            player = _player;
            currentWeapon = GetComponentInChildren<Weapon>();
            movement = GetComponent<Movement>();
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
                currentWeapon.AddAmmo(crate.Ammo);
                crate.DestroyAmmoCrate();
            }
        }

    }
}