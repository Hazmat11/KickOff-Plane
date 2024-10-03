using System.Collections;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
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
}
