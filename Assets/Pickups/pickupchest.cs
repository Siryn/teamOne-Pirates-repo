using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupchest : MonoBehaviour
{
    void FixedUpdate()
    {
        gameObject.transform.Rotate(0, 0, 1);
    }

    void OnTriggerEnter(Collider other)
    {
        //GlobVar.score += 10;
        Destroy(gameObject);
    }
}
