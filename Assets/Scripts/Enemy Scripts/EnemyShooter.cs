﻿using System.Collections;
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
    private ParticleSystem muzzleFireParticleSystem;

    float nextFireAllowed;
    public bool canFire;
    public Transform muzzle;
    public EnemyPlayer enemyPlayer;
    public LayerMask layerMask;
    public int swordDamage;

    public void Equip()
    {
        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    void OnDisable()
    {

    }

    private void Update()
    {
        Debug.DrawRay(gameObject.transform.position, transform.TransformDirection(Vector3.forward), Color.blue);
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

        //bool isLocalPlayerControlled = aimTarget == null;

        muzzle.LookAt(aimTarget.position + aimTargetOffset);

        Projectile newBullet = Instantiate(projectile, muzzle.position, muzzle.rotation);

        FireEffect();
        audioFire.Play();
        canFire = true;
    }

    public virtual void SwordAttack(int damage)
    {
        if (Time.time < nextFireAllowed)
            return;

        nextFireAllowed = Time.time + rateOfFire;

        RaycastHit hit;

        if (Physics.Raycast(gameObject.transform.position, transform.TransformDirection(Vector3.forward), out hit, 2f, layerMask))
        {
            PlayerHealth player = hit.collider.gameObject.GetComponent<PlayerHealth>();
            player.currentHp -= damage;
            player.CheckDeath();
            print("PlayerWouldTakeDamage");

        }
        else
        {
            Transform player = enemyPlayer.priorityTarget.transform;

            enemyPlayer.SetDestinationToPriorityTarget(player);
            print("out of range");
        }
    }
}
