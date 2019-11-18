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
    bool shouldFire;

    private void Start()
    {
        enemyPlayer = GetComponent<EnemyPlayer>();
        enemyPlayer.OnTargetSelected += EnemyPlayer_OnTargetSelected;
    }

    private void EnemyPlayer_OnTargetSelected(PlayerMove target)
    {
        print("OnTargetSelected");
        enemyActiveWeapon.aimTarget = target.transform;
        //enemyActiveWeapon.aimTargetOffset = Vector3.up * 1.5f;
        StartBurst();
    }

    void StartBurst()
    {
        //if (!enemyPlayer.enemyhealth.IsAlive)
           // return;

        CheckReload();
        shouldFire = true;

        //GameManager.Instance.Timer.Add(EndBurst, Random.Range(burstDurationMin, burstDurationMax));
        float random = Random.Range(burstDurationMin, burstDurationMax);
        Invoke("EndBurst", random);
    }

    void EndBurst()
    {
        shouldFire = false;
        //if (!enemyPlayer.enemyhealth.IsAlive)
           // return;
        CheckReload();
        //GameManager.Instance.Timer.Add(StartBurst, shootingSpeed);
        Invoke("StartBurst", shootingSpeed);
    }

    void CheckReload()
    {
        if (enemyActiveWeapon.reloader.roundsRemainingInClip == 0)
            activeWeapon.Reload();
    }

    private void Update()
    {
        if (!shouldFire || !canfire) //|| !enemyPlayer.enemyhealth.IsAlive)
            return;

        enemyActiveWeapon.Fire();
    }
}
