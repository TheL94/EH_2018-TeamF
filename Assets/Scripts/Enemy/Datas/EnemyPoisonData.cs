using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TeamF
{
    [CreateAssetMenu(fileName = "EnemyPoisonData", menuName = "Enemy/EnemyPoisonData")]
    public class EnemyPoisonData : EnemyGenericData
    {
        [Header("Poison Specific Parameters")]
        public GameObject CloudPrefab;
        public float CloudReleaseDistance;
    }
}
