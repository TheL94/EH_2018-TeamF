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

        int ComboMultiplier { get { return GameManager.I.ComboCounter.Count; } set { GameManager.I.ComboCounter.Count = value; } }
        int currentLevelScore;
        int totalScore;       

        #region Event
        public delegate void ScoreCountEvent(ScoreType _type);
        public static ScoreCountEvent OnScoreAction;

        public delegate void ScoreShowEvent(int _value);
        public static ScoreShowEvent OnScoreChange;
        #endregion

        public void Init()
        {
            OnScoreAction += AddScore;
        }

        public void Clear()
        {
            OnScoreAction -= AddScore;
        }

        public void EndRoundAction(LevelEndingStaus _levelEnding)
        {
            if (_levelEnding == LevelEndingStaus.Won)
            {
                totalScore += currentLevelScore;
                WriteBestScore();

                currentLevelScore = 0;
            }
            else if (_levelEnding == LevelEndingStaus.Lost)
            {
                if (SaveScoreOnGameLost)
                {
                    totalScore += currentLevelScore;
                    WriteBestScore();
                }

                currentLevelScore = 0;
                totalScore = 0;
            }
            else
            {
                currentLevelScore = 0;
                totalScore = 0;
            }
        }

        void WriteBestScore()
        {
            if (totalScore > BestScore)
                BestScore = totalScore;
        }

        void AddScore(ScoreType _type)
        {
            int? scoreValue = GetScoreValue(_type);
            if (scoreValue != null)
            {
                if (ComboMultiplier != 0)
                    currentLevelScore += scoreValue.Value * ComboMultiplier;
                else
                    currentLevelScore += scoreValue.Value;

                if (currentLevelScore < 0)
                    currentLevelScore = 0;

                if (OnScoreChange != null)
                    OnScoreChange(currentLevelScore);

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
