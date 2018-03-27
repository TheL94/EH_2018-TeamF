using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public interface IEnemyBehaviour
    {
        float CalulateDamage(float _damage, ElementalType _damageType);
        void DoDeath(ElementalType _bulletType, Vector3 _position);
    }

    public static class IEnemyBehaviourExtension
    {
        /// <summary>
        /// Instanzia il prefab della combo elementale e lancia l'evento per in counter delle combo elementali.
        /// </summary>
        /// <param name="_folderPath">Il percorso all'interno della cartella resources del prefab della combo elementale</param>
        public static void InstantiateElementalCombo(this IEnemyBehaviour _enemyBehaviour, string _folderPath, Vector3 _position)
        {
            GameObject.Instantiate(Resources.Load(_folderPath), _position, Quaternion.identity);
            if (ComboCounter.OnComboCreation != null)
                ComboCounter.OnComboCreation();
        }
    }
}