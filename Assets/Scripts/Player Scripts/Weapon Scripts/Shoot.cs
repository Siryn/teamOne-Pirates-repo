using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shoot : MonoBehaviour
{

    [SerializeField] float rateOfFire;
    [SerializeField] Projectile projectile;
    [SerializeField] Transform hand;
    public Transform aimForNow;
    public AudioController audioReload;
    public AudioController audioFire;

    public Reload reloader;
    public InputController inputController;
    public WeaponController weaponController;
    public States states;

    public ParticleSystem muzzleFireParticleSystem;
   // private bool isPlayerAlive = true;
    private float nextFireAllowed;
    
    public Transform muzzle;
    public bool canFire;

    public Animator playerAnim;

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
        //weaponController = FindObjectOfType<WeaponController>();
        reloader = GetComponent<Reload>();
        canFire = true;
    }

    public void Update()
    {
        if (inputController.reload)
        {

            Reload();
        }

        //if (!isPlayerAlive)
          //  return;

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
            print("worked");
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
        playerAnim.SetBool("isShooting", true);
        Invoke("TestingAnimTime", 0.45f);
        Invoke("ResetShootingAnim", rateOfFire);

    }

    public void ResetShootingAnim()
    {
        playerAnim.SetBool("isShooting", false);
    }

    public void TestingAnimTime()
    {
        Projectile newBullet = Instantiate(projectile, muzzle.position, muzzle.rotation);
        newBullet.transform.LookAt(aimForNow);
        FireEffect();
        audioFire.Play();
    }

}
