using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class LevelManager
    {
        public bool IsGameWon { get; private set; }

        public float PointsToWin;
        float roundPoints;

        GameManager gameMng; 

        public LevelManager(GameManager _gameMng, float _pointsToWin)
        {
            gameMng = _gameMng;
            PointsToWin = _pointsToWin;
        }

        public void UpdateRoundPoints(float _killedEnemyValue)
        {
            roundPoints += _killedEnemyValue;
            EventManager.KillPointsChanged(roundPoints, PointsToWin);

            CheckVictory();              
        }

        public void GameWon()
        {
            IsGameWon = true;
            gameMng.ChangeFlowState(FlowState.EndGame);
        }

        public void GameLost()
        {
            gameMng.ChangeFlowState(FlowState.EndGame);
        }

        void CheckVictory()
        {
            if (roundPoints >= PointsToWin)
                GameWon();
        }
    }
}

