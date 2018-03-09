using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class LevelManager
    {
        public float PointsToWin { get; private set; }
        float roundPoints = 0;

        bool isCurrentLevelMng;         //TODO: al reload della scena viene instanziato un nuovo level manager ma quello vecchio continua ad esistere e ad ascoltare gli eventi che vengono lanciati

        public LevelManager(float _pointsToWin)
        {
            PointsToWin = _pointsToWin;
            Events_LevelController.OnKillPointChanged += UpdateRoundPoints;
            isCurrentLevelMng = true;
        }

        public void UpdateRoundPoints(float _killedEnemyValue)
        {
            if (!isCurrentLevelMng)
                return;

            roundPoints += _killedEnemyValue;
            Events_UIController.KillPointsChanged(roundPoints, PointsToWin);

            CheckVictory();              
        }

        public void GoToGameWon()
        {
            GameManager.I.ChangeFlowState(FlowState.GameWon);
            isCurrentLevelMng = false;

        }

        public void GoToGameLost()
        {
            GameManager.I.ChangeFlowState(FlowState.GameLost);
            isCurrentLevelMng = false;
        }

        void CheckVictory()
        {
            if (roundPoints >= PointsToWin)
                GoToGameWon();
        }

        ~ LevelManager()
        {
            Events_LevelController.OnKillPointChanged -= UpdateRoundPoints;
        }
    }
}

