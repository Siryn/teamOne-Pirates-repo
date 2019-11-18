using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponController : MonoBehaviour
{
    public Shoot activeWeapon;
    public EnemyShooter enemyActiveWeapon;
    public Text weaponText;
    public float weaponSwitchTime;
    public bool canSwitch;

    Shoot[] weapons;


    int currentWeaponIndex;
    public bool canfire;
    Transform weaponHolster;
    public AmmoUI ammoUI;

    private void Awake()
    {
        canfire = true;
        weaponHolster = transform.Find("Weapons");
        weapons = weaponHolster.GetComponentsInChildren<Shoot>();
        if (weapons.Length > 0)
            Equip(0);

    }

    void DeactivateWeaopns()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
            weapons[i].transform.SetParent(weaponHolster);
        }
    }

    internal void SwitchWeapon(int direction)
    {
        if (canSwitch == true)
        {
            canSwitch = false;
            canfire = false;
            currentWeaponIndex += direction;

            if (currentWeaponIndex > weapons.Length - 1)
                currentWeaponIndex = 0;

            if (currentWeaponIndex < 0)
                currentWeaponIndex = weapons.Length - 1;

            Equip(currentWeaponIndex);
        }

    }

    internal void Equip(int index)
    {
        DeactivateWeaopns();
        canfire = true;
        canSwitch = true;
        activeWeapon = weapons[index];
        activeWeapon.Equip();
        weapons[index].gameObject.SetActive(true);
        weaponText.text = weapons[index].name.ToString();
        Reload reloader = weapons[index].GetComponent<Reload>();
        int amountInInventory = reloader.roundsRemainingInInventory;
        int amountInClip = reloader.roundsRemainingInClip;
        ammoUI.text.text = string.Format("{0}/{1}", amountInClip, amountInInventory);
    }
}
