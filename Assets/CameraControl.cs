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
        if (Plane.instance.paused)
            return;
        
        
        rotation.x += Input.GetAxis(xAxis) * sensitivity;
        rotation.y += Input.GetAxis(yAxis) * sensitivity;

        rotation.y = Mathf.Clamp(rotation.y, -40, 10);

        Quaternion xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        Quaternion yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        Quaternion goal = xQuat * yQuat;
        transform.rotation = Quaternion.Slerp(transform.rotation, goal, sensitivity * Time.deltaTime);
    }
}