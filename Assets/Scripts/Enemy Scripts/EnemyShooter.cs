using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{


    [SerializeField] float rateOfFire;
    [SerializeField] Projectile projectile;
    [SerializeField] Transform hand;
    [SerializeField] AudioController audioReload;
    [SerializeField] AudioController audioFire;

    public Transform aimTarget;
    public Vector3 aimTargetOffset;

    public Reload reloader;
    //public InputController inputController;
    private ParticleSystem muzzleFireParticleSystem;

    float nextFireAllowed;
    public bool canFire;
    public Transform muzzle;

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
        reloader = GetComponent<Reload>();
        muzzleFireParticleSystem = muzzle.GetComponent<ParticleSystem>();
    }

   /* public void Update()
    {
        if (inputController.reload)
        {

            Reload();
        }

    }*/

    void FireEffect()
    {
        if (muzzleFireParticleSystem == null)
            return;
        muzzleFireParticleSystem.Play();
    }

    public virtual void Fire()
    {
        canFire = false;

        if (Time.time < nextFireAllowed)
            return;

        if (reloader != null)
        {

            if (reloader.isReloading)
                return;

            if (reloader.roundsRemainingInClip == 0)
                return;

            reloader.TakeFromClipEnemy(1);
        }
        nextFireAllowed = Time.time + rateOfFire;

        bool isLocalPlayerControlled = aimTarget == null;

        muzzle.LookAt(aimTarget.position + aimTargetOffset);

        Projectile newBullet = Instantiate(projectile, muzzle.position, muzzle.rotation);

        FireEffect();
        audioFire.Play();
        canFire = true;
    }
}
