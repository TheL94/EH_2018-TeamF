using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemyBehaviourMelee : IEnemyBehaviour
    {
        public  Enemy myEnemy;

        public virtual void DoInit(Enemy _myEnemy)
        {
            myEnemy = _myEnemy;
        }

        public virtual void DoAttack()
        {
            if (myEnemy.Animator != null)
                myEnemy.AnimState = Enemy.AnimationState.MeleeAttack;
            myEnemy.Target.TakeDamage(myEnemy.Data.Damage);
        }

        public virtual float CalulateDamage(Enemy _enemy, float _damage, ElementalType _type)
        {
            return _damage; 
        }

        public virtual void DoDeath(ElementalType _bulletType)
        {
            // Azioni da compiere alla morte
        }

        /// <summary>
        /// Instanzia il prefab della combo elementale e lancia l'evento per in counter delle combo elementali.
        /// </summary>
        /// <param name="_folderPath">Il percorso all'interno della cartella resources del prefab della combo elementale</param>
        protected void InstantiateElementalCombo(string _folderPath)
        {
            GameObject.Instantiate(Resources.Load(_folderPath), myEnemy.transform.position, Quaternion.identity);
            if (ComboCounter.OnComboCreation != null)
                ComboCounter.OnComboCreation();
        }
    }
}