using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_Poison_ReleaseCloud")]
    public class Enemy_Poison_ReleaseCloud : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            ReleaseCloud((_controller as AI_Enemy).Enemy);
            return true;
        }

        void ReleaseCloud(Enemy _enemy)
        {
            if (!_enemy.AI_Enemy.IsDisengaging)
            {
                EnemyPoisonData data = _enemy.Data as EnemyPoisonData;
                GameObject.Instantiate(data.CloudPrefab, _enemy.transform.position, Quaternion.identity, null);
            }
        }
    }
}
