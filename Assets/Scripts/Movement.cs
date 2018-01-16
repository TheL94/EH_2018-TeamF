using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    Rigidbody rigid;
    public float MovementSpeed = 100;
    public float RotationSpeed = 1;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
	}

    public void Move(Vector3 _position)
    {
        rigid.AddForce(MovementSpeed * _position, ForceMode.Force);
    }

    public void Rotate()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(mouseRay, out floorHit))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;

            playerToMouse.y = 0;

            rigid.MoveRotation(Quaternion.LookRotation(playerToMouse));
        }
    }
}
