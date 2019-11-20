using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPlayer : MonoBehaviour
{

    NavAgent pathFinder;
    [SerializeField] Scanner playerScanner;
    public float walkSpeed = 5;
    public float runSpeed = 10;
    public float damping;

    public PlayerMove priorityTarget;
    List<PlayerMove> myTargets;

    public event System.Action<PlayerMove> OnTargetSelected;

    public EnemyHealth enemyhealth;
    public bool playerInRange;

    EnemyState m_EnemyState;
    public EnemyState enemyState
    {
        get
        {
            if (m_EnemyState == null)
                m_EnemyState = GetComponent<EnemyState>();

            return m_EnemyState;
        }
    }

    void Start()
    {
        pathFinder = GetComponent<NavAgent>();
        pathFinder.Agent.speed = walkSpeed;
        playerScanner.OnScanReady += Scanner_OnScanReady;
        Scanner_OnScanReady();

        //enemyhealth.OnDeath += Enemyhealth_OnDeath;
        enemyState.OnModeChanged += EnemyState_OnModeChanged;
    }

    private void EnemyState_OnModeChanged(EnemyState.EMode state)
    {
        pathFinder.Agent.speed = walkSpeed;

        if (state == EnemyState.EMode.AWARE)
            pathFinder.Agent.speed = runSpeed;
    }

    private void Enemyhealth_OnDeath()
    {

    }

    private void Scanner_OnScanReady()
    {
        if (priorityTarget != null)
            return;

        myTargets = playerScanner.ScanForTargets<PlayerMove>();
        if (myTargets.Count == 1)
        {
            priorityTarget = myTargets[0];
        }
        else
        {
            SelectClosestTarget();
        }

        if (priorityTarget != null)
        {
            if (OnTargetSelected != null)
                OnTargetSelected(priorityTarget);
        }
    }

    public void SetDestinationToPriorityTarget(Transform player)
    {
        Vector3 offSetTarget = player.position;
        offSetTarget.x -= 1.5f;
        pathFinder.SetTarget(offSetTarget);
        playerInRange = true;
    }

    private void SelectClosestTarget()
    {
        float closestTarget = playerScanner.ScanRange;
        foreach (var possibleTarget in myTargets)
        {
            if (Vector3.Distance(transform.position, possibleTarget.transform.position) < closestTarget)
                priorityTarget = possibleTarget;

        }
    }

    private void Update()
    {
        if (priorityTarget == null)
            return;

        Vector3 lookPos = priorityTarget.transform.position - transform.position;
        lookPos.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * damping);
    }
}
