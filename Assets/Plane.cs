using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{

    [SerializeField] private GameObject LookAtObject;
    [SerializeField] private GameObject Bullet;

    private Rigidbody _rigidbody;
    private float speed = 20.0f;
    private float newXRotation;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(LookAtObject.transform);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        ray.origin = transform.position;

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            LookAtObject.transform.position = hit.point + ray.direction * 20;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector3.up * 5, ForceMode.Force);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(Bullet, transform.position + 2 * transform.forward, transform.rotation);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.parent.position += Time.deltaTime * speed * new Vector3(0, 0, transform.forward.z);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.parent.position += Time.deltaTime * speed * new Vector3(Vector3.left.x, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.parent.position += Time.deltaTime * speed * new Vector3(0, 0, Vector3.back.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.parent.position += Time.deltaTime * speed * new Vector3(Vector3.right.x, 0, 0);
        }


        newXRotation = Mathf.Clamp(transform.eulerAngles.x, -40, 40);
        transform.rotation = Quaternion.Euler(newXRotation, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    private void FixedUpdate()
    {
        float newYVelocity = Mathf.Clamp(_rigidbody.velocity.y, -100, 10);


        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, newYVelocity, _rigidbody.velocity.z);
    }
}