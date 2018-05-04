using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class Movement : MonoBehaviour
    {
        public float MovementSpeed { get; set; }
        float RotationSpeed;

        Rigidbody playerRigidbody;

        public void Init(float _movementSpeed, float _rotationSpeed, DashStruct _dashData)
        {
            playerRigidbody = GetComponent<Rigidbody>();
            dashData = _dashData;
            chargeCount = dashData.ChargeCount;
            MovementSpeed = _movementSpeed;
            RotationSpeed = _rotationSpeed;
        }

        public void Move(Vector3 _position)
        {
            Vector3 position = _position * MovementSpeed * Time.deltaTime;
            playerRigidbody.MovePosition(transform.position + position);
        }

        public void Turn()
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;

            if (Physics.Raycast(mouseRay, out floorHit, float.PositiveInfinity, 1 << LayerMask.NameToLayer("MouseRaycast")))
            {
                Vector3 playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0;
                Quaternion newRotatation = Quaternion.LookRotation(playerToMouse, transform.up);
                playerRigidbody.MoveRotation(newRotatation);
            }
        }

        #region Dash
        DashStruct dashData;
        int chargeCount;            // Le cariche di dash eseguibili
        float coolDown;             // Il timer che allo scadere viene rigenerata una tacca di dash

        public void Dash(Vector3 _direction)
        {
            if (chargeCount > 0)
            {
                playerRigidbody.AddRelativeForce(_direction * dashData.DashForce, ForceMode.Impulse);

                if (_direction.x == 0 && _direction.z == 0)
                    playerRigidbody.AddRelativeForce(transform.forward * dashData.DashForce, ForceMode.Impulse);

                chargeCount--;
            }
        }

        private void Update()
        {
            if (chargeCount < dashData.ChargeCount)
            {
                coolDown += Time.deltaTime;
                if (coolDown >= dashData.ChargeCooldown)
                {
                    chargeCount++;
                    coolDown = 0;
                }
            }
        }
        #endregion
    }
}