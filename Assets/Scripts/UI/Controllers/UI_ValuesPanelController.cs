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
        public InputField Character_BulletSpeed;
        public InputField Character_Ratio;
        public Toggle Invincible;
        public Toggle InfiniteAmmo;

        #endregion

        #region Enemy
        EnemyData oldEnemyData;

        public InputField Enemy_Life;
        public InputField Enemy_Damage;
        public InputField Enemy_Speed;
        public InputField Enemy_BulletSpeed;
        public InputField Enemy_AttackRange;
        public Toggle FollowPlayerToggle;
        #endregion
        #endregion

        public void Init(CharacterData _characterData, EnemyData _enemyData)
        {
            oldCharacterData = Instantiate(_characterData);

            Character_Life.text = oldCharacterData.Life.ToString();
            Character_Speed.text = oldCharacterData.MovementSpeed.ToString();
            Character_RotationSpeed.text = oldCharacterData.RotationSpeed.ToString();
            Character_BulletSpeed.text = oldCharacterData.BulletSpeed.ToString();
            Character_Ratio.text = oldCharacterData.Ratio.ToString();

            oldEnemyData =Instantiate(_enemyData);

            Enemy_Life.text = oldEnemyData.Life.ToString();
            Enemy_Damage.text = oldEnemyData.Damage.ToString();
            Enemy_Speed.text = oldEnemyData.Speed.ToString();
            Enemy_AttackRange.text = oldEnemyData.DamageRange.ToString();
            MenuBaseInit();
        }

        void MenuBaseInit()
        {
            GameManager.I.UIMng.CurrentMenu = this;
            FindISelectableObects();
            SelectableButtons[0].IsSelected = true;
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
            newData.BulletSpeed = float.Parse(Character_BulletSpeed.text);
            newData.Ratio = float.Parse(Character_Ratio.text);

            GameManager.I.Player.CharacterData = newData;
        }
        /// <summary>
        /// Setta titti i dati dei nemici che ha l'enemy spawner controller con quelli inseriti negli input field
        /// </summary>
        void SetEnemiesValue()
        {
            foreach (EnemyData data in GameManager.I.EnemyMng.DataInstance.EnemiesData)
            {
                data.Life = float.Parse(Enemy_Life.text);
                data.Damage = int.Parse(Enemy_Damage.text);
                data.Speed = float.Parse(Enemy_Speed.text);
                data.DamageRange = float.Parse(Enemy_AttackRange.text);
            }
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
                    GameManager.I.ChangeFlowState(FlowState.EnterTestScene);
                    break;
                //case 1:
                //    GameManager.I.MenuActions();
                //    break;
            }
        }
    }
}