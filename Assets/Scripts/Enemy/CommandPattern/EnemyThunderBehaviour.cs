using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemyThunderBehaviour : EnemyBehaviourMelee
    {
        public override void DoInit(Enemy _myEnemy)
        {
            base.DoInit(_myEnemy);
            _myEnemy.GetComponentInChildren<MeshRenderer>().material.color = Color.magenta;
        }

        public override void DoTakeDamage(Enemy _enemy, float _damage, ElementalType _type)
        {
            if (_type == ElementalType.Poison)
                _damage *= 1.5f;
            if (_type == ElementalType.Thunder)
                _damage = 0;
            base.DoTakeDamage(_enemy, _damage, _type);
        }

        public override void DoDeath(ElementalType _bulletType)
        {
            if(_bulletType == ElementalType.Fire)
                GameObject.Instantiate(Resources.Load("ElementalCombo/BlackHole"), myEnemy.transform.position, Quaternion.identity);
            //if (_bulletType == ElementalType.Water)
            //    GameObject.Instantiate(Resources.Load("ElementalCombo/ParalyzingCloud"), myEnemy.transform.position, Quaternion.identity);    Effetto da spostare nel nemico d'acqua se colpito dal tuono

            base.DoDeath(_bulletType);
        }
    }
}