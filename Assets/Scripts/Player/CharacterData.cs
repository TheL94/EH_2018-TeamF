using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Character/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        public float Life;
        public ElementalAmmo[] AllElementalAmmo;

        #region Movement variables
        public float MovementSpeed;
        public float RotationSpeed;
        public DashStruct DashValues;
        #endregion

        #region Weapon
        public int MagCapacity;
        public float BulletSpeed;
        public float Ratio;
        #endregion

        #region Bullet
        public List<BulletData> BulletDatas = new List<BulletData>();
        #endregion
    }

    [System.Serializable]
    public struct DashStruct
    {
        public float DashDinstance;
        public int ChargeCount;
        public float ChargeCooldown;
    }
}