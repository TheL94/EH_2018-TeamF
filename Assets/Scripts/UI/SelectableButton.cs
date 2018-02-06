using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamF
{
    public class SelectableButton : MonoBehaviour, ISelectable
    {
        public int Index { get; set; }

        bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                ChangeImageIfSelected(_isSelected);
            }
        }

        public void ChangeImageIfSelected(bool _isSelected)
        {
            Image img = GetComponent<Image>();
            if (_isSelected)
                img.color = Color.grey;
            else
                img.color = Color.white;

        }

        public void SetIndex(int _index)
        {
            Index = _index;
        }
    }
}