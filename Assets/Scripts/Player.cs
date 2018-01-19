using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Movement movement;
    Weapon currentWeapon;
    public int Life;

	void Start ()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
        movement = GetComponent<Movement>();
    }
	
	void FixedUpdate ()
    {
        Movement();
    }

    void Movement()
    {
        if (Life <= 0)
            return;

        if (Input.GetKey(KeyCode.W))
            movement.Move(transform.forward);
        if (Input.GetKey(KeyCode.S))
            movement.Move(-transform.forward);
        if (Input.GetKey(KeyCode.A))
            movement.Move(-transform.right);
        if (Input.GetKey(KeyCode.D))
            movement.Move(transform.right);
        if (Input.GetMouseButtonDown(0))
            currentWeapon.Shot();

        movement.Rotate();
    }

    public void TakeDamage(int _damage)
    {
        Life -= _damage;
        if (Life <= 0)
            Destroy(movement.ModelToRotate);
    }
}
