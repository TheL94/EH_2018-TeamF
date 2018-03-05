using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TeamF
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "Bullet/BulletData")]
    public class BulletData : ScriptableObject
    {
        public ElementalType Type;
        public GameObject BulletContainerPrefab;
        public GameObject BulletGraphicPrefab;
        public GameObject BulletTrailPrefab;
    }
}

