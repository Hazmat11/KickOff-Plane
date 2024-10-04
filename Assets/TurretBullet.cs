using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{

    public float lifeTime = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Hit");

        if (other.gameObject.tag == "Player")
        {
            Plane.instance.TakeDamage();
        }
        Destroy(gameObject);
    }
}
