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
                            ScoreCounter.OnScoreAction(ScoreType.EnemyMeleeKill, _enemy.transform.position);
                            break;
                        case EnemyType.Ranged:
                            ScoreCounter.OnScoreAction(ScoreType.EnemyRangedKill, _enemy.transform.position);
                            break;
                        case EnemyType.Fire:
                            ScoreCounter.OnScoreAction(ScoreType.EnemyFireKill, _enemy.transform.position);
                            break;
                        case EnemyType.Water:
                            ScoreCounter.OnScoreAction(ScoreType.EnemyWaterKill, _enemy.transform.position);
                            break;
                        case EnemyType.Poison:
                            ScoreCounter.OnScoreAction(ScoreType.EnemyPoisonKill, _enemy.transform.position);
                            break;
                        case EnemyType.Thunder:
                            ScoreCounter.OnScoreAction(ScoreType.EnemyThunderKill, _enemy.transform.position);
                            break;
                    }                
                }
                return false;
            }

            return true;
        }
    }
}
