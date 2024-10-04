using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public GameObject target;
    // Start is called before the first frame update

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
        
        transform.LookAt(target.transform);
    }
}
