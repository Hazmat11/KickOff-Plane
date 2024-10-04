using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float Sensitivity
    {
        get { return sensitivity; }
        set { sensitivity = value; }
    }

    private float _newXRotation;

    [Range(0.1f, 9f)][SerializeField] float sensitivity = 2f;

    Vector2 rotation = Vector2.zero;
    const string xAxis = "Mouse X";
    const string yAxis = "Mouse Y";

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        rotation.x += Input.GetAxis(xAxis) * sensitivity;
        rotation.y += Input.GetAxis(yAxis) * sensitivity;
        Quaternion xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        Quaternion yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        transform.rotation = xQuat * yQuat;

        if (transform.eulerAngles.x >= 180)
        {
            _newXRotation = Mathf.Clamp(transform.eulerAngles.x, 350, 360);
        }
        else if (transform.eulerAngles.x <= 180)
        {
            _newXRotation = Mathf.Clamp(transform.eulerAngles.x, 0, 40);
        }

        transform.rotation = Quaternion.Euler(_newXRotation, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}