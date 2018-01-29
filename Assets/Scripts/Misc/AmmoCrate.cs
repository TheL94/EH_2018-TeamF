using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrate : MonoBehaviour {

    public int Ammo;

    public void DestroyAmmoCrate()
    {
        gameObject.SetActive(false);
    }

}
