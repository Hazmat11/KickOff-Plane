using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    public Camera playerCam;

    public float forwardSpeed;

    public float sidewaysSpeed;
    public float verticalSpeed;

    public float mouseXSensitivity;
    public float mouseYSensitivity;

    public Transform refCam;

    public Rigidbody rb;

    public static DebugController instance;

    public bool paused;

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(this);
        }

        instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        playerCam = Camera.main;
        rb = GetComponent<Rigidbody>();
        paused = true;
    }

    // Update is called once per frame

    private Vector3 input;
    void Update()
    {
        if (paused) return;
        
        transform.rotation *= Quaternion.Euler(0,
            mouseXSensitivity * Input.GetAxis("Mouse X")
            ,0);
        

        input = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            input += transform.forward * forwardSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            input += -transform.forward * forwardSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            input += -transform.right * sidewaysSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            input += transform.right * sidewaysSpeed;
        }


        
        if (Input.GetKey(KeyCode.Space))
        {
            input = new Vector3(input.x, verticalSpeed, input.z);
        }    
        if (Input.GetKey(KeyCode.LeftShift))
        {
            input = new Vector3(input.x, -verticalSpeed, input.z);
        }

        rb.velocity = input;

    }
}
