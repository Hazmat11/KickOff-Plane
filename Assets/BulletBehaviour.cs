using System.Collections;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject impact;

    private float _speed = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ToDestroy());
    }

    private void FixedUpdate()
    {
        transform.position += Time.deltaTime * _speed * transform.forward;
    }

    IEnumerator ToDestroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(impact, collision.GetContact(0).point, transform.rotation);
    }
}
