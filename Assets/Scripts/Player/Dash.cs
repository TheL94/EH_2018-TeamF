using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class Dash : MonoBehaviour
    {
        Movement movement;

        public void Init(Movement _movement)
        {
            movement = _movement;
        }

        public void ActivateDash()
        {
            Vector3 newPos = movement.ModelToRotate.transform.forward;  //transform.worldToLocalMatrix.MultiplyVector(transform.forward); ;
            newPos *= 20;

            movement.Move(newPos);
        }
    }
}