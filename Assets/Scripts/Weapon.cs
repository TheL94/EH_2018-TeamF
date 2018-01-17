using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public GameObject BulletPrefab;
    public float BulletSpeed;
    public void Shot()
    {
       Bullet bull = Instantiate(BulletPrefab, transform.position, transform.rotation).GetComponent<Bullet>();
       bull.Speed = BulletSpeed;
    }

}
