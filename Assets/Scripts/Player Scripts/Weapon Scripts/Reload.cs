﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//////////////////////////////////////////////////////
// Project: Major Project 1: Dam Buster
//Name: Andrew Fletcher
//Section: 2019FA.SGD.212.4103
//Instructor: Aisha Eskandari
// Date: 09/15/2019
//////////////////////////////////////////////////////

public class Reload : MonoBehaviour
{
    public int maxAmmo;
    public float reloadTime;
    public int clipSize;
    public Inventory inventory;
    public WeaponEnum weaponType;
    public AmmoUI ammoUI;
    public int shotsFiredInClip;
    public bool isReloading;
    public WeaponController weaponController;
    System.Guid containerItemId;

    public event System.Action OnAmmoChanged;

    public int roundsRemainingInClip
    {
        get
        {
            return clipSize - shotsFiredInClip;
        }
    }

    public int roundsRemainingInInventory
    {
        get
        {
            return inventory.GetAmountRemaining(containerItemId);
        }
    }


    public void Awake()
    {
        inventory.OnContainerReady += () => { containerItemId = inventory.Add(weaponType.ToString(), maxAmmo); };
        weaponController = FindObjectOfType<WeaponController>();
    }

    public void Reloading()
    {
        if (isReloading)
        {
            print("isReloading already");
            return;
        }

        isReloading = true;
        print("Reload started");
        weaponController.canSwitch = false;
        StartCoroutine(ExecuteReload(inventory.TakeFromContainer(containerItemId, clipSize - roundsRemainingInClip)));


    }

    private IEnumerator ExecuteReload(int amount)
    {
        yield return new WaitForSeconds(reloadTime);

        print("Reload executed!");
        isReloading = false;
        shotsFiredInClip -= amount;
        ammoUI.HandleOnAmmoChanged();
        weaponController.canSwitch = true;
    }

    public void TakeFromClip(int amount)
    {
        shotsFiredInClip += amount;
        ammoUI.HandleOnAmmoChanged();
    }

    public void HandleOnAmmoChanged()
    {
        if (OnAmmoChanged != null)
            OnAmmoChanged();
    }
}
