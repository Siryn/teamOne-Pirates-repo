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

public class Crosshair : MonoBehaviour
{
    public float speed;
    public Transform reticle;

    private Transform crossTop;
    private Transform crossBottom;
    private Transform crossLeft;
    private Transform crossRight;
    private float reticleStartPoint;


    private void Start()
    {
        crossTop = reticle.Find("Cross/Top").transform;
        crossBottom = reticle.Find("Cross/Bottom").transform;
        crossLeft = reticle.Find("Cross/Left").transform;
        crossRight = reticle.Find("Cross/Right").transform;

        reticleStartPoint = crossTop.localPosition.y;
    }

    private void Update()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        reticle.transform.position = Vector3.Lerp(reticle.transform.position, screenPosition, speed * Time.deltaTime);
    }
}
