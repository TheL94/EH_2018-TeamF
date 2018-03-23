using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_DropElementalCombo")]
    public class Enemy_DropElementalCombo : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return DropElementalCombo((_controller as AI_Enemy).Enemy);
        }

        bool DropElementalCombo(Enemy _enemy)
        {
            _enemy.CurrentBehaviour.DoDeath(_enemy.LastHittingBulletType, _enemy.Position);
            return true;
        }
    }
}
