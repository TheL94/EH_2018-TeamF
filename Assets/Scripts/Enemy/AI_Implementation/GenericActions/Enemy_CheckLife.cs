using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_CheckLife")]
    public class Enemy_CheckLife : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return CheckLife((_controller as AI_Enemy).Enemy);
        }

        bool CheckLife(Enemy _enemy)
        {
            if (_enemy.Life <= 0)
            {
                if (ScoreCounter.OnScoreAction != null)
                {
                    switch (_enemy.Data.EnemyType)
                    {
                        case EnemyType.Melee:
                            ScoreCounter.OnScoreAction(ScoreType.EnemyMeleeKill);
                            break;
                        case EnemyType.Ranged:
                            ScoreCounter.OnScoreAction(ScoreType.EnemyRangedKill);
                            break;
                        case EnemyType.Fire:
                            ScoreCounter.OnScoreAction(ScoreType.EnemyFireKill);
                            break;
                        case EnemyType.Water:
                            ScoreCounter.OnScoreAction(ScoreType.EnemyWaterKill);
                            break;
                        case EnemyType.Poison:
                            ScoreCounter.OnScoreAction(ScoreType.EnemyPoisonKill);
                            break;
                        case EnemyType.Thunder:
                            ScoreCounter.OnScoreAction(ScoreType.EnemyThunderKill);
                            break;
                    }                
                }
                return false;
            }

            return true;
        }
    }
}
