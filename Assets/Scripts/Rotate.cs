using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private void FixedUpdate()
    {
        gameObject.transform.Rotate(-1, 0, 0);
    }
}
