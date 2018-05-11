using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class PoisonCloud : MonoBehaviour
    {
        public void Init(float _lifeTime)
        {
            StartCoroutine(ObscuringCloudLifeTime(_lifeTime, gameObject));
        }

        IEnumerator ObscuringCloudLifeTime(float _lifeTime, GameObject _cloud)
        {
            yield return new WaitForSeconds(_lifeTime);
            DestroyObject(_cloud);
        }
    }
}

