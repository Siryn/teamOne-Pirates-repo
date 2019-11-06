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

public class States : MonoBehaviour
{
    public enum EMoveState
    {
        WALKING,
        RUNNING,
        CROUCHING,
        SPRINTING
    }

    public enum EWeaponState
    {
        IDLE,
        FIRING,
        AIMING,
        AIMEDFIRING
    }

    public EMoveState moveState;
    public EWeaponState weaponState;
    public InputController inputController;

    private void Start()
    {
        inputController = GetComponent<InputController>();   
    }

    void Update()
    {
        SetMoveState();
        SetWeaponState();
    }

    void SetWeaponState()
    {
        weaponState = EWeaponState.IDLE;

        if (inputController.fire1)
            weaponState = EWeaponState.FIRING;

        if (inputController.fire2)
            weaponState = EWeaponState.AIMING;

        if (inputController.fire1 && inputController.fire2)
            weaponState = EWeaponState.AIMEDFIRING;
    }

    void SetMoveState()
    {
        moveState = EMoveState.RUNNING;

        if (inputController.isSpriting)
            moveState = EMoveState.SPRINTING;

        if (inputController.isWalking)
            moveState = EMoveState.WALKING;

        if (inputController.isCrouched)
            moveState = EMoveState.CROUCHING;
    }
}
