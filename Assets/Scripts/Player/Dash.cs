using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class Dash : MonoBehaviour
    {
        DashStruct dashData;
        Movement movement;

        int chargeCount;            // Le cariche di dash eseguibili
        float coolDown;             // Il timer che allo scadere viene rigenerata una tacca di dash

        public void Init(Movement _movement, DashStruct _data)
        {
            movement = _movement;
            dashData = _data;
            chargeCount = dashData.ChargeCount;
        }

        private void Update()
        {
            if(chargeCount < dashData.ChargeCount)
            {
                coolDown += Time.deltaTime;
                if(coolDown >= dashData.ChargeCooldown)
                {
                    chargeCount++;
                    coolDown = 0;
                }
            }
        }

        public void ActivateDash(Vector3 _direction)
        {
            if (chargeCount > 0)
            {
                movement.Dash(_direction, dashData.DashDinstance);

                if (_direction.x == 0 && _direction.z == 0)
                {
                    Vector3 newPos = movement.ModelToRotate.transform.forward;
                    movement.Dash(newPos, dashData.DashDinstance);
                }
                chargeCount--; 
            }
        }
    }
}