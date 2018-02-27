using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class Movement : MonoBehaviour
    {
        public GameObject ModelToRotate;
        float MovementSpeed;
        float RotationSpeed;

        public void Init(float _movementSpeed, float _rotationSpeed)
        {
            MovementSpeed = _movementSpeed;
            RotationSpeed = _rotationSpeed;
        }

        public void Move(Vector3 _position)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + _position, MovementSpeed * Time.deltaTime);
        }

        public void Rotate()
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;

            if (Physics.Raycast(mouseRay, out floorHit, float.PositiveInfinity, 1 << LayerMask.NameToLayer("MouseRaycast")))
            {
                Vector3 playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0;
                ModelToRotate.transform.rotation = Quaternion.Slerp(ModelToRotate.transform.rotation, Quaternion.LookRotation(playerToMouse, transform.up), RotationSpeed * Time.deltaTime);
            }
        }
    }
}