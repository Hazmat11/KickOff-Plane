using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Plane : MonoBehaviour
{
    [SerializeField] private GameObject lookAtObject;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject wings;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject[] gunPoints;
    [SerializeField] private Slider fill;

    private Rigidbody _rigidbody;
    private float _speed = 20.0f;
    private float _cooldown = 0.1f;
    private bool _leftOrRight;
    private int _rightOrLeft;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        wings.transform.Rotate(0, 360 * Time.deltaTime, 0);
        for (int i = 0; i < gunPoints.Length; i++)
        {
            gunPoints[i].transform.LookAt(lookAtObject.transform);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        ray.origin = transform.position;

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            lookAtObject.transform.position = hit.point + ray.direction * 20;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            wings.transform.Rotate(0, 1000 * Time.deltaTime, 0);
            _rigidbody.AddForce(Vector3.up * 5, ForceMode.Force);
        }

        if (Input.GetMouseButton(0))
        {
            if (_cooldown <= 0)
            {
                _rightOrLeft = _leftOrRight == true ? 0 : 1;
                gunPoints[_rightOrLeft].transform.Rotate(0, 0, 360 * Time.deltaTime);
                Instantiate(bullet, gunPoints[_rightOrLeft].transform.position + transform.forward, gunPoints[_rightOrLeft].transform.rotation);
                _leftOrRight = !_leftOrRight;
                _cooldown = 0.1f;
            }
            else { _cooldown -= Time.deltaTime; }
        }

        InputFunction();
    }

    private void InputFunction()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Time.deltaTime * _speed * transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Time.deltaTime * _speed * transform.right * -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Time.deltaTime * _speed * transform.forward * -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Time.deltaTime * _speed * transform.right;
        }
    }

    private void FixedUpdate()
    {
        float newYVelocity = Mathf.Clamp(_rigidbody.velocity.y, -2, 2);
        float newYPosition = Mathf.Clamp(transform.position.y, -5, 9);
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, newYVelocity, _rigidbody.velocity.z);
        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Instantiate(explosion, collision.GetContact(0).point, transform.rotation, transform);
            fill.value -= 0.2f;
            if (fill.value <= 0)
            { Destroy(gameObject); }
        }
    }
}