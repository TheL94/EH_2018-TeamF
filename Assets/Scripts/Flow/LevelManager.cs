using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class LevelManager
    {
        public float PointsToWin;
        float roundPoints;

        public LevelManager(float _pointsToWin)
        {
            PointsToWin = _pointsToWin;
            Events_LevelController.OnKillPointChanged += UpdateRoundPoints;
        }

        public void UpdateRoundPoints(float _killedEnemyValue)
        {
            roundPoints += _killedEnemyValue;
            Events_UIController.KillPointsChanged(roundPoints, PointsToWin);

            CheckVictory();              
        }

        public void GoToGameWon()
        {
            GameManager.I.ChangeFlowState(FlowState.GameWon);
        }

        public void GoToGameLost()
        {
            GameManager.I.ChangeFlowState(FlowState.GameLost);
        }

        void CheckVictory()
        {
            if (roundPoints >= PointsToWin)
                GoToGameWon();
        }
    }
}

