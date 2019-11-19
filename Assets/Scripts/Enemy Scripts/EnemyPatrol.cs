using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{

    [SerializeField] WaypointController wayPointController;
    [SerializeField] float waitTimeMin;
    [SerializeField] float waitTimeMax;

    NavAgent pathFinder;

    public EnemyShoot enemyShoot;

    EnemyPlayer m_EnemyPlayer;
    public EnemyPlayer enemyPlayer
    {
        get
        {
            if (m_EnemyPlayer == null)
                m_EnemyPlayer = GetComponent<EnemyPlayer>();
            return m_EnemyPlayer;
        }
    }

    private void Start()
    {
        wayPointController.SetNextWaypoint();
    }

    private void Awake()
    {
        pathFinder = GetComponent<NavAgent>();
        pathFinder.OnDestinationReached += PathFinder_OnDestinationReached;
        wayPointController.OnWayPointChanged += WayPointController_OnWayPointChanged;

        //enemyPlayer.enemyhealth.OnDeath += Enemyhealth_OnDeath;
        enemyPlayer.OnTargetSelected += EnemyPlayer_OnTargetSelected;
    }

    private void EnemyPlayer_OnTargetSelected(PlayerMove obj)
    {
        //HERE IS ERROR YOU ARE LOOKING FOR
        if (enemyShoot.hasSword)
        {

        }
        else
        {
            pathFinder.Agent.isStopped = true;
        }
    }

    private void Enemyhealth_OnDeath()
    {
        pathFinder.Agent.isStopped = true;
    }

    private void WayPointController_OnWayPointChanged(WayPoint waypoint)
    {
        pathFinder.SetTarget(waypoint.transform.position);
    }

    private void PathFinder_OnDestinationReached()
    {
        if (enemyPlayer.playerInRange == false)
        {
            //assume we are patrolling
            //GameManager.Instance.Timer.Add(wayPointController.SetNextWaypoint, UnityEngine.Random.Range(waitTimeMin, waitTimeMax));
            float random = UnityEngine.Random.Range(waitTimeMin, waitTimeMax);
            Invoke("NextWayPoint", random);
        }
    }

    public void NextWayPoint()
    {
        wayPointController.SetNextWaypoint();
    }


}