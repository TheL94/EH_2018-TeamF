using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ComboCounter : MonoBehaviour
    {
        int _count;
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                if (OnCounterChanged != null)
                    OnCounterChanged(Count);
            }
        }
        bool isActive;
        float timer;
        float startTimer;

        bool isInitialized = false;

        public void Init(float _comboTimer)
        {
            if (!isInitialized)
            {
                OnComboCreation += ActivateCount;
                isInitialized = true;
            }
            startTimer = _comboTimer;
        }

        public void Clear()
        {
            Count = 0;
            isActive = false;
            isInitialized = false;
            OnComboCreation -= ActivateCount;
        }

        private void Update()
        {
            if (!isActive)
                return;

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                isActive = false;
                Count = 0;
            }
        }

        void ActivateCount()
        {
            if (!isActive)
                isActive = true;
            Count++;
            timer = startTimer;
        }

        #region UI CounterEvent
        public delegate void ComboCounterUIEvent(int _count);
        public static ComboCounterUIEvent OnCounterChanged;
        public delegate void ComboCounterElementalDeath();
        public static ComboCounterElementalDeath OnComboCreation;
        #endregion
    }
}