using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private float speed = 40.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ToDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position += Time.deltaTime * speed * transform.forward;
    }

    IEnumerator ToDestroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
