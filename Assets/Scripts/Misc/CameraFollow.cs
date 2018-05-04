using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform Target;
        public float MovemntSmoothing = 5f;

        Vector3 offset;

        void Start()
        {
            offset = transform.position - Target.position;
        }

        void FixedUpdate()
        {
            Vector3 targetCamPos = Target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, MovemntSmoothing * Time.deltaTime);
        }
    }
}

