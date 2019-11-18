using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour
{

    WayPoint[] waypoints;

    int currentWaypointIndex = -1;

    public event System.Action<WayPoint> OnWayPointChanged;

    private void Awake()
    {
        waypoints = GetWaypoints();
    }

    public void SetNextWaypoint()
    {
        currentWaypointIndex++;

        if (currentWaypointIndex == waypoints.Length)
            currentWaypointIndex = 0;

        if (OnWayPointChanged != null)
            OnWayPointChanged(waypoints[currentWaypointIndex]);
    }

    WayPoint[] GetWaypoints()
    {
        return GetComponentsInChildren<WayPoint>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 previousWaypoint = Vector3.zero;
        foreach (var waypoint in GetWaypoints())
        {
            Vector3 waypointPosition = waypoint.transform.position;
            Gizmos.DrawWireSphere(waypointPosition, .2f);

            if (previousWaypoint != Vector3.zero)
                Gizmos.DrawLine(previousWaypoint, waypointPosition);

            previousWaypoint = waypointPosition;
        }
    }
}
