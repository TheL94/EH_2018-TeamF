using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Movement movement;
    Weapon currentWeapon;
    // Use this for initialization
	void Start () {
        currentWeapon = GetComponentInChildren<Weapon>();
        movement = GetComponent<Movement>();
    }
	
	// Update is called once per frame
	void Update () {
        Movement();
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.W))
            movement.Move(1);
        if (Input.GetKey(KeyCode.S))
            movement.Move(-1);
        if (Input.GetKey(KeyCode.D))
            movement.Rotate(1);
        if (Input.GetKey(KeyCode.A))
            movement.Rotate(-1);
        if (Input.GetMouseButtonDown(0))
            currentWeapon.Shot();
    }
}
