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

    public void Move(float _value)
    {
        rigid.AddRelativeForce(Vector3.forward * MovementSpeed * _value, ForceMode.Force);
    }

    public void Rotate(float _value)
    {
        rigid.AddRelativeTorque(Vector3.up * RotationSpeed * _value, ForceMode.Force);
    }
}
