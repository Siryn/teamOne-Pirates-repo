using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyPlayer))]
public class EnemyShoot : WeaponController
{

    [SerializeField] float shootingSpeed;
    [SerializeField] float burstDurationMax;
    [SerializeField] float burstDurationMin;

    EnemyPlayer enemyPlayer;
    EnemyHealth enemyHealth;
    bool shouldFire;
    public bool hasSword;
    public Animator anim;

    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        enemyPlayer = GetComponent<EnemyPlayer>();
        enemyPlayer.OnTargetSelected += EnemyPlayer_OnTargetSelected;
        anim.SetBool("isWalking", true);
    }

    private void EnemyPlayer_OnTargetSelected(PlayerMove target)
    {
        print("OnTargetSelected");
        enemyActiveWeapon.aimTarget = target.transform;
        StartBurst();
    }

    void StartBurst()
    {
        if (enemyHealth.enemyIsDead == true)
            return;

        CheckReload();
        shouldFire = true;

        if (hasSword)
        {
            Invoke("EndBurst", .4f);
        }
        else
        {
            float random = Random.Range(burstDurationMin, burstDurationMax);
            Invoke("EndBurst", random);
        }
    }

    void EndBurst()
    {
        shouldFire = false;
        if (enemyHealth.enemyIsDead == true)
            return;
        CheckReload();
        Invoke("StartBurst", shootingSpeed);
    }

    void CheckReload()
    {
        if (enemyActiveWeapon.reloader.roundsRemainingInClip == 0)
            activeWeapon.Reload();
    }

    private void Update()
    {
        if (!shouldFire || !canfire || enemyHealth.enemyIsDead == true)
            return;
        if (hasSword)
        {
            enemyActiveWeapon.SwordAttack(enemyActiveWeapon.swordDamage);
            anim.SetBool("isWalking", false);
            anim.SetBool("isSlashing", true);
            Invoke("ResetAnimBools", 0.5f);
        }
        else
        {
            Invoke("TestAnimShootTiming", 1.2f);
            anim.SetBool("isWalking", false);
            anim.SetBool("isShooting", true);
            Invoke("ResetAnimBools", 1.3f);
        }
    }

    public void ResetAnimBools()
    {
        if(hasSword)
        {
            anim.SetBool("isWalking", true);
        }
        anim.SetBool("isSlashing", false);
        anim.SetBool("isShooting", false);
    }

    public void TestAnimShootTiming()
    {
        enemyActiveWeapon.Fire();
    }
}
