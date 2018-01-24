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
	
	void Update ()
    {
        CheckInput();
    }

    void CheckInput()
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
            currentWeapon.SingleShot();
        if (Input.GetMouseButton(0))
            currentWeapon.FullAutoShoot();

        movement.Rotate();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        AmmoCrate crate = other.GetComponent<AmmoCrate>();
        if (crate != null)
        {
            currentWeapon.AddAmmo(crate.Ammo);
            crate.DestroyAmmoCrate();
        }
    }

    public void TakeDamage(int _damage)
    {
        Life -= _damage;
        if (Life <= 0)
        {
            Destroy(movement.ModelToRotate);
            GameManager.I.ChageFlowState(FlowState.EndGame);
        }
    }
}
