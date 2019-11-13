using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AmmoUI : MonoBehaviour
{
    public Text text;

    public Shoot shoot;
    public Reload reloader;
    public WeaponController weaponController;
    public GameObject hand;
    public InputController inputController;

    public void Start()
    {
        shoot = hand.GetComponentInChildren<Shoot>();
        reloader = hand.GetComponentInChildren<Reload>();
        Invoke("HandleOnAmmoChanged", 0.2f);
    }

    public void Update()
    {
        if (inputController.mouseWheelDown || inputController.mouseWheelUp)
        {
            shoot = hand.GetComponentInChildren<Shoot>();
            reloader = hand.GetComponentInChildren<Reload>();
            HandleOnAmmoChanged();
        }

    }

    public void HandleOnWeaponSwitch(Shoot activeWeapon)
    {
        reloader = activeWeapon.reloader;
        reloader.OnAmmoChanged += HandleOnAmmoChanged;
        HandleOnAmmoChanged();
    }

    public void HandleOnAmmoChanged()
    {
        int amountInInventory = reloader.roundsRemainingInInventory;
        int amountInClip = reloader.roundsRemainingInClip;
        text.text = string.Format("{0}/{1}", amountInClip, amountInInventory);
    }
}
