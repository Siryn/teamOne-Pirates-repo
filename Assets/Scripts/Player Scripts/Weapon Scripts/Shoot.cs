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

public class Shoot : MonoBehaviour
{

    [SerializeField] float rateOfFire;
    [SerializeField] Projectile projectile;
    [SerializeField] Transform hand;
    public AudioController audioReload;
    public AudioController audioFire;

    public Transform aimTarget;
    public Vector3 aimTargetOffset;

    public Reload reloader;
    public InputController inputController;
    public WeaponController weaponController;
    public States states;

    public ParticleSystem muzzleFireParticleSystem;
    private bool isPlayerAlive = true;
    private float nextFireAllowed;
    
    public Transform muzzle;
    public bool canFire;

    public void Equip()
    {
        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    void OnDisable()
    {

    }

    public void Reload()
    {
        if (reloader == null)
        {

            return;
        }
        reloader.Reloading();
        audioReload.Play();

    }

    void Awake()
    {
        weaponController = FindObjectOfType<WeaponController>();
        reloader = GetComponent<Reload>();
    }

    public void Update()
    {
        if (inputController.reload)
        {

            Reload();
        }

        if (!isPlayerAlive)
            return;

        if (inputController.mouseWheelDown)
            weaponController.SwitchWeapon(1);

        if (inputController.mouseWheelUp)
            weaponController.SwitchWeapon(-1);

        if (states.moveState == States.EMoveState.SPRINTING)
            return;

        if (!weaponController.canfire)
            return;

        if (inputController.fire1 && canFire == true)
        {
            weaponController.activeWeapon.Fire();
        }

    }

    void FireEffect()
    {
        if (muzzleFireParticleSystem == null)
            return;
        muzzleFireParticleSystem.Play();
    }

    public virtual void Fire()
    {

        if (Time.time < nextFireAllowed)
            return;

        if (reloader.isReloading)
            return;

        if (reloader.roundsRemainingInClip == 0)
        {
            Reload();
            return;
        }

        reloader.TakeFromClip(1);

        nextFireAllowed = Time.time + rateOfFire;


        Projectile newBullet = Instantiate(projectile, muzzle.position, muzzle.rotation);

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;
        Vector3 targetPosition = ray.GetPoint(500);
        if (Physics.Raycast(ray, out hit))
            targetPosition = hit.point;

        newBullet.transform.LookAt(targetPosition);

        FireEffect();
        audioFire.Play();

    }

}
