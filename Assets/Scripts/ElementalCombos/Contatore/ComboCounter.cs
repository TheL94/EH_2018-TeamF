using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ComboCounter : MonoBehaviour
    {
        int counter;
        bool isActive;
        float timer;
        float startTimer;

        public void Init(float _comboTimer)
        {
            OnComboCreation += ActivateCount;
            startTimer = _comboTimer;
        }

        private void Update()
        {
            if (!isActive)
                return;

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                isActive = false;
                counter = 0;
                if (OnCounterChanged != null)
                    OnCounterChanged(counter);
            }
        }
        
        void ActivateCount()
        {
            if (!isActive)
                isActive = true;
            counter++;
            timer = startTimer;
            if (OnCounterChanged != null)
                OnCounterChanged(counter);
        }

        #region UI CounterEvent
        public delegate void ComboCounterUIEvent(int _count);
        public static ComboCounterUIEvent OnCounterChanged;
        public delegate void ComboCounterElementalDeath();
        public static ComboCounterElementalDeath OnComboCreation;
        #endregion
    }
}