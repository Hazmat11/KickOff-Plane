using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public GameObject target;

    private Vector3 basePos;

    public float bounceAmplitude;
    public float bounceFrequency = 0.05f;

    private void Awake()
    {
        basePos = transform.localPosition;
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

            if (Vector3.Distance(target.transform.position, Plane.instance.transform.position) >
                Vector3.Distance(go.transform.position, Plane.instance.transform.position))
            {
                target = go;
            }
        }

        transform.LookAt(target.transform);
        transform.localPosition = Vector3.Lerp(basePos, basePos + transform.forward * bounceAmplitude, Mathf.Sin(Time.time * bounceFrequency) *0.5f + 0.5f);
    }
}
