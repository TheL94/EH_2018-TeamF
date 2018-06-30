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
        public Toggle Invincible;
        public Toggle InfiniteAmmo;

        #endregion

        #region Enemy
        public InputField Enemy_Life;
        public InputField Enemy_Damage;
        public InputField Enemy_Speed;
        public InputField Enemy_BulletSpeed;
        public InputField Enemy_AttackRange;
        public Toggle FollowPlayerToggle;
        #endregion

        public InputField SceneToLoad;
        #endregion

        public CharacterData CharacterDataForTestScene;
        public EnemyGenericData EnemyGenericDataForTestScene;

        public void Init(CharacterData _characterData, EnemyGenericData _enemyData)
        {
            CharacterDataForTestScene = Instantiate(_characterData);

            Character_Life.text = CharacterDataForTestScene.Life.ToString();
            Character_Speed.text = CharacterDataForTestScene.MovementSpeed.ToString();
            Character_RotationSpeed.text = CharacterDataForTestScene.RotationSpeed.ToString();

            EnemyGenericDataForTestScene = Instantiate(_enemyData);

            Enemy_Life.text = EnemyGenericDataForTestScene.Life.ToString();
            Enemy_Damage.text = EnemyGenericDataForTestScene.MeleeDamage.ToString();
            Enemy_Speed.text = EnemyGenericDataForTestScene.Speed.ToString();
            Enemy_AttackRange.text = EnemyGenericDataForTestScene.MeleeDamageRange.ToString();

            SceneToLoad.text = "1";

            GameManager.I.UIMng.CurrentMenu = this;
            base.Init();
        }

        /// <summary>
        /// Sostituisce il dato del player con uno contenente i valori settati nel menu
        /// </summary>
        void SetCharacterValues()
        {
            CharacterDataForTestScene.Life = float.Parse(Character_Life.text);
            CharacterDataForTestScene.MovementSpeed = float.Parse(Character_Speed.text);
            CharacterDataForTestScene.RotationSpeed = float.Parse(Character_RotationSpeed.text);
        }

        /// <summary>
        /// Setta titti i dati dei nemici che ha l'enemy spawner controller con quelli inseriti negli input field
        /// </summary>
        void SetEnemiesValue()
        {
            EnemyGenericDataForTestScene.Life = float.Parse(Enemy_Life.text);
            EnemyGenericDataForTestScene.MeleeDamage = float.Parse(Enemy_Damage.text);
            EnemyGenericDataForTestScene.Speed = float.Parse(Enemy_Speed.text);
            EnemyGenericDataForTestScene.MeleeDamageRange = float.Parse(Enemy_AttackRange.text);
        }

        public override void Select()
        {
            switch (CurrentIndexSelected)
            {
                case 0:
                    SetCharacterValues();
                    SetEnemiesValue();
                    GameManager.I.LevelMng.MapIndex = int.Parse(SceneToLoad.text);
                    break;
            }
            base.Select();
        }
    }
}