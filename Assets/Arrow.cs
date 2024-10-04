using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public GameObject target;

    private Vector3 basePos;

    public float bounceAmplitude;

    private void Awake()
    {
        basePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        target = null;
        foreach (var go in GameManager.instance.enemies)
        {
            if (!target)
            {
                target = go;
                continue;
            }

            if (Vector3.Distance(target.transform.position, DebugController.instance.transform.position) >
                Vector3.Distance(go.transform.position, DebugController.instance.transform.position))
            {
                target = go;
            }
        }

        transform.position = Vector3.Lerp(basePos, basePos + transform.forward * bounceAmplitude, Mathf.Sin(Time.time));
        transform.LookAt(target.transform);
    }
}
