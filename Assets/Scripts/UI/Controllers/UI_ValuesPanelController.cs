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
        CharacterData oldCharacterData;

        public InputField Character_Life;
        public InputField Character_Damage;
        public InputField Character_Speed;
        public InputField Character_RotationSpeed;
        public Toggle Invincible;
        public Toggle InfiniteAmmo;

        #endregion

        #region Enemy
        EnemyGenericData oldEnemyData;

        public InputField Enemy_Life;
        public InputField Enemy_Damage;
        public InputField Enemy_Speed;
        public InputField Enemy_BulletSpeed;
        public InputField Enemy_AttackRange;
        public Toggle FollowPlayerToggle;
        #endregion

        public InputField SceneToLoad;
        #endregion

        public void Init(CharacterData _characterData, EnemyGenericData _enemyData)
        {
            oldCharacterData = Instantiate(_characterData);

            Character_Life.text = oldCharacterData.Life.ToString();
            Character_Speed.text = oldCharacterData.MovementSpeed.ToString();
            Character_RotationSpeed.text = oldCharacterData.RotationSpeed.ToString();

            oldEnemyData =Instantiate(_enemyData);

            Enemy_Life.text = oldEnemyData.Life.ToString();
            Enemy_Damage.text = oldEnemyData.MeleeDamage.ToString();
            Enemy_Speed.text = oldEnemyData.Speed.ToString();
            Enemy_AttackRange.text = oldEnemyData.MeleeDamageRange.ToString();

            SceneToLoad.text = "1";

            GameManager.I.UIMng.CurrentMenu = this;
            base.Init();
        }

        /// <summary>
        /// Sostituisce il dato del player con uno contenente i valori settati nel menu
        /// </summary>
        void SetCharacterValues()
        {
            CharacterData newData = oldCharacterData;
            newData.Life = float.Parse(Character_Life.text);
            newData.MovementSpeed = float.Parse(Character_Speed.text);
            newData.RotationSpeed = float.Parse(Character_RotationSpeed.text);

            GameManager.I.Player.CharacterData = newData;
        }

        /// <summary>
        /// Setta titti i dati dei nemici che ha l'enemy spawner controller con quelli inseriti negli input field
        /// </summary>
        void SetEnemiesValue()
        {
            for (int i = 0; i < GameManager.I.EnemyMng.DataInstance.EnemiesData.Count; i++)
            {
                EnemyGenericData enemyInstanceData = Instantiate(GameManager.I.EnemyMng.DataInstance.EnemiesData[i]);
                enemyInstanceData.Life = float.Parse(Enemy_Life.text);
                enemyInstanceData.MeleeDamage = int.Parse(Enemy_Damage.text);
                enemyInstanceData.Speed = float.Parse(Enemy_Speed.text);
                enemyInstanceData.MeleeDamageRange = float.Parse(Enemy_AttackRange.text);
                GameManager.I.EnemyMng.DataInstance.EnemiesData[i] = enemyInstanceData;
            }

            //foreach (EnemyData data in GameManager.I.EnemyMng.DataInstance.EnemiesData)
            //{
            //    EnemyData instanceData = Instantiate(data);
            //    instanceData.Life = float.Parse(Enemy_Life.text);
            //    instanceData.Damage = int.Parse(Enemy_Damage.text);
            //    instanceData.Speed = float.Parse(Enemy_Speed.text);
            //    instanceData.DamageRange = float.Parse(Enemy_AttackRange.text);

            //}
        }

        public override void Select()
        {
            switch (CurrentIndexSelected)
            {
                case 0:
                    SetCharacterValues();
                    SetEnemiesValue();
                    EnemyManager mng = GameManager.I.EnemyMng;
                    (mng as EnemySpawner_TS).FollowPlayer = FollowPlayerToggle.isOn;
                    GameManager.I.LevelMng.Level = int.Parse(SceneToLoad.text);
                    break;
            }
        }
    }
}