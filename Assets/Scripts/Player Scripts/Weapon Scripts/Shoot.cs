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
    public AudioSource swordSlash;

    public Reload reloader;
    public InputController inputController;
    public WeaponController weaponController;
    public States states;
    public EnemyHealth enemyHealth;

    public ParticleSystem muzzleFireParticleSystem;
   // private bool isPlayerAlive = true;
    private float nextFireAllowed;
    
    public Transform muzzle;
    public bool canFire;

    public Animator playerAnim;

    public int swordDamage;

    public HandgunReload hgReload;

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
        //audioReload.Play();

    }

    void Awake()
    {
        //reloader = GetComponent<Reload>();
        canFire = true;
    }

    public void Update()
    {

        /*if (inputController.reload)
        {

            Reload();
        }*/

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

        if (inputController.fire1 && canFire == true && weaponController.activeWeapon.name == "Sword")
        {
            weaponController.activeWeapon.SwordAttack();
            //print("sword attack");
            return;
        }

        if (inputController.fire1 && canFire == true)
        {
            weaponController.activeWeapon.Fire();
            //print("worked");
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
        Invoke("ShootAtAnimTime", 0.45f);
        Invoke("ResetShootingAnim", rateOfFire);

    }

    public void ResetShootingAnim()
    {
        playerAnim.SetBool("isShooting", false);
    }

    public void ShootAtAnimTime()
    {
        Projectile newBullet = Instantiate(projectile, muzzle.position, muzzle.rotation);
        newBullet.transform.LookAt(aimForNow);
        FireEffect();
        audioFire.Play();
        if (reloader.shotsFiredInClip == 3)
        {
            //reloader.Reloading();
            hgReload.ReloadHandGun();
            weaponController.SwitchWeapon(1);
            //weaponController.canSwitch = false;
            Invoke("CannotSwitchWeaopns", 0.1f);
        }
    }

    public virtual void SwordAttack()
    {
        if (Time.time < nextFireAllowed)
            return;

        nextFireAllowed = Time.time + rateOfFire;
        //print("sword did attack");
        playerAnim.SetBool("swordAttack", true);
        swordSlash.Play();
        weaponController.SwordDamage();
        Invoke("ResetSwordBool", rateOfFire);
    }

    public void ResetSwordBool()
    {
        playerAnim.SetBool("swordAttack", false);
    }

    public void CannotSwitchWeaopns()
    {
        weaponController.canSwitch = false;
    }

}
