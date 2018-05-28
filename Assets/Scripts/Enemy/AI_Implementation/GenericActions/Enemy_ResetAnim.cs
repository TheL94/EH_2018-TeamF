using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_ResetAnim")]
    public class Enemy_ResetAnim : AI_Action
    {
        float timer = 0;
        protected override bool Act(AI_Controller _controller)
        {
            timer += Time.deltaTime;

            if (ResetAnimation((_controller as AI_Enemy).Enemy))
                return true;

            return false;
        }

        bool ResetAnimation(Enemy _enemy)
        {
            _enemy.transform.position = new Vector3(_enemy.transform.position.x, _enemy.transform.position.y - timer, _enemy.transform.position.z);
            if (timer >= 2)
                _enemy.GetComponentInChildren<Animator>().Play("Idle");
            if (timer >= 5)
                return true;

            return false;
        }
    }
}
