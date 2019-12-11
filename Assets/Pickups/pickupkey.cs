using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupkey : MonoBehaviour
{
    void FixedUpdate()
    {
        gameObject.transform.Rotate(0, -1, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        //GlobVar.hasKey = true;
        Destroy(gameObject);
    }
}
