using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamF
{
    public class UI_ValuesPanelController : MenuBase
    {
        #region Variables Fields
        #region Character
        public InputField Character_Life;
        public InputField Character_Damage;
        public InputField Character_Speed;
        public InputField Character_RotationSpeed;
        public InputField Character_BulletSpeed;
        public InputField Character_Ratio;
        public Toggle Invincible;
        public Toggle InfiniteAmmo;

        #endregion

        #region Enemy
        public InputField Enemy_Life;
        public InputField Enemy_Damage;
        public InputField Enemy_Speed;
        public InputField Enemy_BulletSpeed;
        public InputField Enemy_AttackRange;
        #endregion
        #endregion

        public void Init(CharacterData _characterData, EnemyData _enemyData)
        {
            Character_Life.text = _characterData.Life.ToString();
            Character_Speed.text = _characterData.MovementSpeed.ToString();
            Character_RotationSpeed.text = _characterData.RotationSpeed.ToString();
            Character_BulletSpeed.text = _characterData.BulletSpeed.ToString();
            Character_Ratio.text = _characterData.Ratio.ToString();

            Enemy_Life.text = _enemyData.Life.ToString();
            Enemy_Damage.text = _enemyData.Damage.ToString();
            Enemy_Speed.text = _enemyData.Speed.ToString();
            Enemy_AttackRange.text = _enemyData.DamageRange.ToString();
        }

        /// <summary>
        /// Sostituisce il dato del player con uno contenente i valori settati nel menu
        /// </summary>
        void SetCharacterValues()
        {
            CharacterData newData = new CharacterData();
            newData.Life = float.Parse(Character_Life.text);
            newData.MovementSpeed = float.Parse(Character_Speed.text);
            newData.RotationSpeed = float.Parse(Character_RotationSpeed.text);
            newData.BulletSpeed = float.Parse(Character_BulletSpeed.text);
            newData.Ratio = float.Parse(Character_Ratio.text);

            GameManager.I.Player.Character.Data = newData;
        }
        /// <summary>
        /// Setta titti i dati dei nemici che ha l'enemy spawner controller con quelli inseriti negli input field
        /// </summary>
        void SetEnemiesValue()
        {
            foreach (EnemyData data in GameManager.I.EnemyMng.Data.EnemiesData)
            {
                data.Life = float.Parse(Enemy_Life.text);
                data.Damage = int.Parse(Enemy_Damage.text);
                data.Speed = float.Parse(Enemy_Speed.text);
                data.DamageRange = float.Parse(Enemy_AttackRange.text);
            }
        }
    }
}