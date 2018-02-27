using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/FollowPath")]
    public class FollowTarget : AI_Action
    {
        public float TargetRefreshTime;

        protected override bool Act(AI_Controller _controller)
        {
            // da rivedere
            if ((_controller as AI_Enemy).Enemy.Target.Life <= 0 || (_controller as AI_Enemy).Enemy.Target == null)
                return true;
            if (Vector3.Distance((_controller as AI_Enemy).Enemy.Agent.destination, (_controller as AI_Enemy).Enemy.Position) <= (_controller as AI_Enemy).Enemy.Agent.stoppingDistance)
                return true;
            
            UpdateTargetPosition((_controller as AI_Enemy).Enemy);
            return false;
        }

        float timeCounter;

        void UpdateTargetPosition(Enemy enemy)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= TargetRefreshTime)
            {
                enemy.Agent.SetDestination(enemy.Target.Position);
                timeCounter = 0;
            }
        }
    }
}

