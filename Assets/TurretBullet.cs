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
        Destroy(gameObject, 10);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Hit");
        Destroy(gameObject);
    }
}
