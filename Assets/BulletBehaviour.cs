using System;
using System.Collections;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject impact;

    private float _speed = 100.0f;
    public float _speed = 20.0f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(ToDestroy());
        transform.LookAt(Plane.instance.lookAtObject.transform);
        rb.velocity = transform.forward * _speed;
    }

    private void FixedUpdate()
    {
        transform.position += Time.deltaTime * _speed * transform.forward;
    }


    private void OnCollisionStay(Collision other)
    {
        if (other.transform.TryGetComponent(out BuildingToDestroy building))
        {
            GameManager.instance.RemoveEnemy(building.gameObject);
        }
        if (other.transform.TryGetComponent(out TurretBehavior turret))
        {
            GameManager.instance.RemoveEnemy(turret.gameObject);
        }
        
        
        Destroy(gameObject);
    }

    IEnumerator ToDestroy()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(impact, collision.GetContact(0).point, transform.rotation);
    }
}
