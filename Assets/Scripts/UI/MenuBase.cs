using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class MenuBase : MonoBehaviour, IMenu, IButtonController
    {
        int _currentIndexSelected;
        public int CurrentIndexSelected
        {
            get { return _currentIndexSelected; }
            set
            {
                _currentIndexSelected = value;
                for (int i = 0; i < SelectableButtons.Count; i++)
                {
                    if (SelectableButtons[i].Index == value)
                        SelectableButtons[i].IsSelected = true;
                    else
                        SelectableButtons[i].IsSelected = false;
                }
            }
        }

        List<ISelectable> _selectableButtons = new List<ISelectable>();

        public List<ISelectable> SelectableButtons
        {
            get { return _selectableButtons; }
            set { _selectableButtons = value; }
        }

        public virtual void Init()
        {
            FindISelectableObects();

            for (int i = 0; i < SelectableButtons.Count; i++)
            {
                if (SelectableButtons[i].IsSelected == true)
                    SelectableButtons[i].IsSelected = false;
            }

            SelectableButtons[0].IsSelected = true;
        }

        /// <summary>
        /// Cerca tutti i selectable button figli e se li salva in un lista
        /// </summary>
        public virtual void FindISelectableObects()
        {
            if (SelectableButtons.Count > 0)
                SelectableButtons.Clear();

            foreach (ISelectable item in GetComponentsInChildren<ISelectable>())
            {
                SelectableButtons.Add(item);
            }

            for (int i = 0; i < SelectableButtons.Count; i++)
            {
                SelectableButtons[i].Init(i, this);
            }
        }

        /// <summary>
        /// Called by the selectable button.. select himself and call the select
        /// </summary>
        /// <param name="_buttonID"></param>
        public void ButtonClick(int _buttonID)
        {
            CurrentIndexSelected = _buttonID;
            Select();
        }

        #region Menu Actions

        public virtual void GoDownInMenu()
        {
            if (SelectableButtons.Count > 0)
                CurrentIndexSelected++;
            if (CurrentIndexSelected > SelectableButtons.Count - 1)
                CurrentIndexSelected = 0;
        }

        public virtual void GoUpInMenu()
        {
            if (SelectableButtons.Count > 0)
                CurrentIndexSelected--;
            if (CurrentIndexSelected < 0)
                CurrentIndexSelected = SelectableButtons.Count - 1;
        }

        public virtual void GoLeftInMenu()
        {
            throw new System.NotImplementedException();
        }

        public virtual void GoRightInMenu()
        {
            throw new System.NotImplementedException();
        }


        public virtual void Select()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}