using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Character/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        [Header("General")]
        public float Life;

        [Header("Bullets")]
        public BulletData[] BulletDatas;

        #region Movement variables
        [Header("Movement")]
        public float MovementSpeed;
        public float RotationSpeed;
        public DashStruct DashValues;
        #endregion

        //#region Bullet
        //public List<BulletData> BulletDatas = new List<BulletData>();
        //#endregion
    }

    [System.Serializable]
    public struct DashStruct
    {
        public float DashDinstance;
        public int ChargeCount;
        public float ChargeCooldown;
    }
}