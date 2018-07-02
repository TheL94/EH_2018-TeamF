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

        GameObject graphic;

        void Start()
        {
            timer = Timer;

            GameManager.I.LevelMng.Combos.Add(this);

            if (ComboCounter.OnComboCreation != null)
                ComboCounter.OnComboCreation();

            if (GraphicID != null || GraphicID != string.Empty)
                graphic = GameManager.I.PoolMng.GetObject(GraphicID);

            if (graphic != null)
            {
                graphic.transform.position = transform.position;
                graphic.transform.SetParent(transform);
            }

            OnInit();
        }

        void Update()
        {
            OnUpdate();
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                EndEffect();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            OnEnterCollider(other);
        }

        private void OnTriggerExit(Collider other)
        {
            OnExitCollider(other);
        }

        public void EndEffect()
        {
            OnEndEffect();
            GameManager.I.LevelMng.Combos.Remove(this);
            GameManager.I.PoolMng.ReturnObject(GraphicID, graphic);
            Destroy(gameObject);
        }

        /// <summary>
        /// Le azioni da svolegere una volta scaduto il timer
        /// </summary>
        protected virtual void OnEndEffect() { }

        protected virtual void OnInit() { }

        protected virtual void OnUpdate() { }
        
        /// <summary>
        /// L'effetto che combiono le classi che ereditano da questa classe
        /// </summary>
        /// <param name="other"></param>
        protected virtual void OnEnterCollider(Collider other) { }

        protected virtual void OnExitCollider(Collider other) { }
    }
}