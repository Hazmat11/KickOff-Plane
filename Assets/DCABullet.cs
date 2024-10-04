using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DCABullet : MonoBehaviour
{
    private Vector3 _heading;

    public GameObject Target;

    void Start()
    {
        _heading = Target.transform.position - transform.position;
        GetComponent<Rigidbody>().AddForce(Vector3.up * 5 + _heading, ForceMode.Impulse);
        StartCoroutine(ToDestroy());
    }

    void FixedUpdate()
    {
    }

    IEnumerator ToDestroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Plane.instance.TakeDamage();
            Instantiate(Plane.instance.explosion, collision.GetContact(0).point, transform.rotation);
        }
        Destroy(gameObject);
    }
}
