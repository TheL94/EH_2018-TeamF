using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Movement movement;
    Weapon currentWeapon;
    public int Life;
    float ratioTimer = 0;

	void Start ()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
        movement = GetComponent<Movement>();
    }
	
	void FixedUpdate ()
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
            currentWeapon.Shot();
        if(Input.GetMouseButton(0))
        {
            ratioTimer += Time.deltaTime;
            if(ratioTimer >= currentWeapon.Ratio)
            {
                currentWeapon.Shot();
                ratioTimer = 0;
            }
        }

        if (Input.GetMouseButtonUp(0))
            ratioTimer = 0;

        movement.Rotate();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        AmmoCrate crate = other.GetComponent<AmmoCrate>();
        if (crate != null)
        {
            currentWeapon.Ammo += crate.Ammo;
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
