using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    Rigidbody rigid;
    public float Speed{ get; set; }
    public float BulletLife;
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        Destroy(this.gameObject, BulletLife);
    }

    void Update () {
        rigid.AddRelativeForce(Vector3.up * Speed, ForceMode.Impulse);
	}
}
