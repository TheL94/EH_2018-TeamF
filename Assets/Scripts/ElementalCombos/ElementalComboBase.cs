using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ElementalComboBase : MonoBehaviour
    {
        public string GraphicID;
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

        protected virtual void DoInit() {
            GameObject obj = GameManager.I.PoolMng.GetObject(GraphicID);
            if (obj != null)
            {
                obj.transform.position = transform.position;
                obj.transform.SetParent(transform);
            }
        }

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
            gameObject.SetActive(false);
            GameManager.I.PoolMng.UpdatePool(GraphicID);
            Destroy(gameObject);
        }

        protected virtual void OnExitCollider(Collider other) { }
    }
}