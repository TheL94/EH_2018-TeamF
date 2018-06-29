using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TeamF
{
    public class SelectableButton : MonoBehaviour, ISelectable, IPointerClickHandler
    {
        public int Index { get; set; }

        public Sprite ButtonOn;
        public Sprite ButtonOff;

        public IButtonController Controller { get; set; }

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

        public void Init(int _index, IButtonController _controller)
        {
            Index = _index;
            Controller = _controller;
        }

        public void ChangeImageIfSelected(bool _isSelected)
        {
            Image img = GetComponent<Image>();
            if (_isSelected)
                img.sprite = ButtonOn;
            else
                img.sprite = ButtonOff;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Controller.ButtonClick(Index);
        }
    }
}