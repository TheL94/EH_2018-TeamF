using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemyPoisonBehaviour : EnemyBehaviourMelee
    {
        public override void DoInit(Enemy _myEnemy)
        {
            base.DoInit(_myEnemy);
            _myEnemy.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
        }

        public override float CalulateDamage(Enemy _enemy, float _damage, ElementalType _type)
        {
            if(_type == ElementalType.Fire)
                _damage *= 1.5f;
            if (_type == ElementalType.Poison)
                _damage = 0;
            return base.CalulateDamage(_enemy, _damage, _type);
        }

        public override void DoDeath(ElementalType _bulletType)
        {
            if(_bulletType == ElementalType.Fire)
                GameObject.Instantiate(Resources.Load("ElementalCombo/FireExplosion"), myEnemy.transform.position, Quaternion.identity);
            if (_bulletType == ElementalType.Water  )
                GameObject.Instantiate(Resources.Load("ElementalCombo/IncreaseDamage"), myEnemy.transform.position, Quaternion.identity);

            base.DoDeath(_bulletType);
        }
    }
}