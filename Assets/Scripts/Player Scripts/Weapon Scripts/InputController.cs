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

public class InputController : MonoBehaviour
{
    public float vertical;
    public float horizontal;
    public Vector2 mouseInput;
    public bool fire1;
    //public bool fire2;
    public bool reload;
    public bool isWalking;
    public bool isSpriting;
    public bool isCrouched;
    public bool mouseWheelUp;
    public bool mouseWheelDown;

    //public PlayerHealth playerHealth;

    public void Start()
    {
       // playerHealth = GetComponent<PlayerHealth>();
    }

    public void Update()
    {
       // if (playerHealth.isAlive == true)
       // {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
            mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            fire1 = Input.GetButton("Fire1");
            //fire2 = Input.GetButton("Fire2");
            reload = Input.GetKey(KeyCode.R);
            isWalking = Input.GetKey(KeyCode.LeftAlt);
            isSpriting = Input.GetKey(KeyCode.LeftShift);
            isCrouched = Input.GetKey(KeyCode.C);
            mouseWheelUp = Input.GetAxis("Mouse ScrollWheel") > 0;
            mouseWheelDown = Input.GetAxis("Mouse ScrollWheel") < 0;
       // }
    }
}
