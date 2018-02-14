using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class LevelManager
    {
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

        public void GoToGameWon()
        {
            gameMng.ChangeFlowState(FlowState.GameWon);
        }

        public void GoToGameLost()
        {
            gameMng.ChangeFlowState(FlowState.GameLost);
        }

        void CheckVictory()
        {
            if (roundPoints >= PointsToWin)
                GoToGameWon();
        }
    }
}

