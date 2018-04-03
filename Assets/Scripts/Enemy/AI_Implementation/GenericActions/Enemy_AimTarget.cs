using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_AimTarget")]
    public class Enemy_AimTarget : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return AimTarget((_controller as AI_Enemy).Enemy);
        }
        
        bool AimTarget(Enemy _enemy)
        {
            Quaternion rotationToReach = Quaternion.LookRotation(_enemy.Target.Position - _enemy.transform.position, Vector3.up);
            _enemy.transform.rotation = Quaternion.Slerp(_enemy.transform.rotation, rotationToReach, _enemy.Data.AimTime);
            return true;
        }
    }
}

