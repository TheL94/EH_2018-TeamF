using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public GameObject ModelToRotate;
    public float MovementSpeed = 1;
    public float RotationSpeed = 1;

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
