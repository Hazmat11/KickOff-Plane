using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DCABullet : MonoBehaviour
{
    private float _startTime;
    private float _speed = 60.0f;
    private bool _stopSin = false;
    private Vector3 _heading;

    public GameObject Target;

    void Start()
    {
        _startTime = Time.time;
        _heading = Target.transform.position - transform.position;
        GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.Impulse);
        StartCoroutine(ToDestroy());
    }

    void FixedUpdate()
    {
        transform.position += Time.deltaTime * _speed * _heading.normalized;
    }

    IEnumerator ToDestroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
