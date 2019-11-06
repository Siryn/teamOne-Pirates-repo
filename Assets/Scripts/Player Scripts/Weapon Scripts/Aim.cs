using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//////////////////////////////////////////////////////
// Project: Major Project 1: Dam Buster
//Name: Andrew Fletcher
//Section: 2019FA.SGD.212.4103
//Instructor: Aisha Eskandari
// Date: 09/15/2019
//////////////////////////////////////////////////////

public class Aim : MonoBehaviour
{
    public float minAngle;
    public float maxAngle;

    public void SetRotation(float amount)
    {
        float clampAngle = GetClamedAngle(amount);
        transform.eulerAngles = new Vector3(clampAngle, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    private float GetClamedAngle(float amount)
    {
        float newAngle = CheckAngle(transform.eulerAngles.x - amount);
        float clampAngle = Mathf.Clamp(newAngle, minAngle, maxAngle);
        return clampAngle;
    }

    public float GetAngle()
    {
        return CheckAngle(transform.eulerAngles.x);
    }

    public float CheckAngle(float value)
    {
        float angle = value - 180;

        if (angle > 0)
            return angle - 180;

        return angle + 180;
    }
}
