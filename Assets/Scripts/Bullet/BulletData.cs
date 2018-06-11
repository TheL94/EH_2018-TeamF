using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TeamF
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "Bullet/BulletData")]
    public class BulletData : ScriptableObject
    {
        [Header("Ammo")]
        public ElementalAmmo ElementalAmmo;
        public int TotalAmmo;

        [Header("")]
        public GameObject BulletContainerPrefab;
        public GameObject BulletGraphicPrefab;
        public GameObject BulletTrailPrefab;
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

