using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class PoisonCloud : MonoBehaviour
    {
        public string GraphicID;
        GameObject graphicObj;

        private void Update()
        {
            if(GameManager.I.CurrentState == FlowState.EndRound)
            {
                StopAllCoroutines();
                Clear();
            }
        }

        public void Init(float _lifeTime)
        {
            if (GraphicID != null || GraphicID != string.Empty)
                graphicObj = GameManager.I.PoolMng.GetObject(GraphicID);

            if (graphicObj != null)
            {
                graphicObj.transform.position = transform.position;
                graphicObj.transform.SetParent(transform);
            }

            StartCoroutine(ObscuringCloudLifeTime(_lifeTime));
        }

        void Clear()
        {
            GameManager.I.PoolMng.ReturnObject(GraphicID, graphicObj);
            Destroy(gameObject);
        }

        IEnumerator ObscuringCloudLifeTime(float _lifeTime)
        {
            yield return new WaitForSeconds(_lifeTime);
            Clear();
        }
    }
}

