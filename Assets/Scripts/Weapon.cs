using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public GameObject BulletPrefab;

    public void Shot()
    {
       Instantiate(BulletPrefab, transform.position, transform.rotation);
    }

}
