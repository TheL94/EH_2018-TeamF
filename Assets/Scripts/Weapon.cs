using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public GameObject BulletPrefab;
    public float BulletSpeed;
    public float Ratio;

    private int _ammo = 10;

    public int Ammo
    {
        get { return _ammo; }
        set { _ammo = value; }
    }


    public void Shot()
    {
        if(Ammo > 0)
        {
            Bullet bull = Instantiate(BulletPrefab, transform.position, transform.rotation).GetComponent<Bullet>();
            bull.Speed = BulletSpeed;
            Ammo--;
        }
    }

}
