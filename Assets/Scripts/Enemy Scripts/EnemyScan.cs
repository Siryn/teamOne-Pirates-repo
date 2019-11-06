using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScan : MonoBehaviour
{
    public Transform[] waypoints;

    public int current = 0;

    public float speed = 7;
    public float waypointRadius = 1;

    public bool playedOnce;
    public float radius = 20;
    public LayerMask mask;

    public bool foundPlayer;

    public Transform player;

    void Start()
    {
        playedOnce = false;
        StartCoroutine(CheckPlayer());
    }
    void FixedUpdate()
    {
        if (foundPlayer == false)
        {


            if (Vector3.Distance(waypoints[current].position, transform.position) < waypointRadius)
            {
                current++;
                if (current >= waypoints.Length)
                {
                    current = 0;
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, waypoints[current].position, Time.deltaTime * speed);
        }

        else if(foundPlayer == true)
        {
            Vector3 playerPosition = player.position;
            playerPosition.x -= 1;

            transform.position = Vector3.MoveTowards(transform.position, playerPosition, Time.deltaTime * speed);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    IEnumerator CheckPlayer()
    {
        while (playedOnce == false)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, mask);
            if (hitColliders.Length != 0 && playedOnce == false)
            {
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    if (hitColliders[i].gameObject.tag == "Player")
                    {
                        playedOnce = true;
                        foundPlayer = true;
                    }
                }
            }
            yield return new WaitForSeconds(.1f);
        }
    }
}
