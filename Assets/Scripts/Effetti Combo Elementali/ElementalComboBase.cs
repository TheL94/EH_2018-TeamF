using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ElementalComboBase : MonoBehaviour
    {

        public float Timer;
        float timer;

        // Use this for initialization
        void Start()
        {
            timer = Timer;
            DoInit();
        }

        // Update is called once per frame
        void Update()
        {
            OnUpdate();
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                OnEndEffect();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            OnEnteringCollider(other);
        }

        private void OnTriggerExit(Collider other)
        {
            OnExitCollider(other);
        }

        protected virtual void DoInit() { }

        protected virtual void OnUpdate() { }
        
        /// <summary>
        /// L'effetto che combiono le classi che ereditano da questa classe
        /// </summary>
        /// <param name="other"></param>
        protected virtual void OnEnteringCollider(Collider other) { }

        /// <summary>
        /// Le azioni da svolegere una volta scaduto il timer
        /// </summary>
        protected virtual void OnEndEffect()
        {
            Destroy(gameObject);
        }

        protected virtual void OnExitCollider(Collider other) { }
    }
}