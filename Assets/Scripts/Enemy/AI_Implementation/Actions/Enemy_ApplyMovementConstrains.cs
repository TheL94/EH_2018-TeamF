using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_ApplyMovementConstrains")]
    public class Enemy_ApplyMovementConstrains : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return ApplyMovementConstrains((_controller as AI_Enemy).Enemy);
        }

        bool ApplyMovementConstrains(Enemy _enemy)
        {
            if (_enemy.transform.rotation.x != 0)
                _enemy.transform.eulerAngles = new Vector3(0, _enemy.transform.eulerAngles.y, _enemy.transform.eulerAngles.z);

            if (_enemy.transform.rotation.z != 0)
                _enemy.transform.eulerAngles = new Vector3(_enemy.transform.eulerAngles.x, _enemy.transform.eulerAngles.y, 0);

            return true;
        }
    }
}

