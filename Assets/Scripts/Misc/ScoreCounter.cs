using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ScoreCounter : MonoBehaviour
    {
        public bool SaveScoreOnGameLost;
        public List<ScoreStruct> ScoreList = new List<ScoreStruct>();

        public int BestScore { get { return PlayerPrefs.GetInt(prefsID); } private set { PlayerPrefs.SetInt(prefsID, value); } }
        const string prefsID = "PlayerBestScore";

        public int LastPartialScore { get; private set; }
        public int CurrentLevelScore { get; private set; }
        public int TotalScore { get; private set; }

        int ComboMultiplier { get { return GameManager.I.ComboCounter.Count; } set { GameManager.I.ComboCounter.Count = value; } }

        HPScript scoreIndicator;

        #region Event
        public delegate void ScoreCountEvent(ScoreType _type, Vector3 _position);
        public static ScoreCountEvent OnScoreAction;

        public delegate void ScoreShowEvent(int _value);
        public static ScoreShowEvent OnScoreChange;
        #endregion

        public void Init()
        {
            OnScoreAction += AddScore;
            scoreIndicator = GetComponent<HPScript>();
        }

        public void Clear()
        {
            OnScoreAction -= AddScore;
        }

        public void EndRoundAction(LevelEndingStaus _levelEnding)
        {
            if (_levelEnding == LevelEndingStaus.Won)
            {
                TotalScore += CurrentLevelScore;
                WriteBestScore();
            }
            else if (_levelEnding == LevelEndingStaus.Lost)
            {
                if (SaveScoreOnGameLost)
                {
                    TotalScore += CurrentLevelScore;
                    WriteBestScore();
                }

                TotalScore = 0;
            }
            else
            {
                TotalScore = 0;
            }

            LastPartialScore = CurrentLevelScore;
            CurrentLevelScore = 0;

        }

        void WriteBestScore()
        {
            if (TotalScore > BestScore)
                BestScore = TotalScore;
        }

        void AddScore(ScoreType _type, Vector3 _position)
        {
            int? scoreValue = GetScoreValue(_type);
            if (scoreValue != null)
            {
                if (ComboMultiplier != 0)
                    CurrentLevelScore += scoreValue.Value * ComboMultiplier;
                else
                    CurrentLevelScore += scoreValue.Value;

                if (CurrentLevelScore < 0)
                    CurrentLevelScore = 0;

                if (OnScoreChange != null)
                    OnScoreChange(CurrentLevelScore);

                scoreIndicator.ChangeHP(_position + new Vector3(0, 5, 0), Color.white, scoreValue.ToString());

                if (_type == ScoreType.PlayerDamage)
                    ComboMultiplier = 0;
            }
        }

        int? GetScoreValue(ScoreType _type)
        {
            foreach (ScoreStruct item in ScoreList)
                if (item.Type == _type)
                    return item.Value;

            return null;
        }
    }

    [System.Serializable]
    public struct ScoreStruct
    {
        public ScoreType Type;
        public int Value;
    }

    public enum ScoreType
    {
        EnemyMeleeKill,
        EnemyRangedKill,
        EnemyFireKill,
        EnemyPoisonKill,
        EnemyThunderKill,
        EnemyWaterKill,
        EnemyDamage,
        EnemyComboEffet,
        PlayerDamage
    }
}
