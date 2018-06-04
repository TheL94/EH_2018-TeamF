using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class PoisonCloud : MonoBehaviour
    {
        public string GraphicID;

        public void Init(float _lifeTime)
        {
            GameObject obj = null;
            if (GraphicID != null || GraphicID != string.Empty)
                obj = GameManager.I.PoolMng.GetObject(GraphicID);

            if (obj != null)
            {
                obj.transform.position = transform.position;
                obj.transform.SetParent(transform);
            }

            StartCoroutine(ObscuringCloudLifeTime(_lifeTime));
        }

        IEnumerator ObscuringCloudLifeTime(float _lifeTime)
        {
            yield return new WaitForSeconds(_lifeTime);
            GameManager.I.PoolMng.UpdatePool(GraphicID);
            Destroy(gameObject);
        }
    }
}

