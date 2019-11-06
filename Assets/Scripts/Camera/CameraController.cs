using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float xMax;
    public float yMax;
    public float xMin;
    public float yMin;
    public float offset;

    public Transform target;

    void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp((target.position.x + offset), xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax), transform.position.z);
    }
}
