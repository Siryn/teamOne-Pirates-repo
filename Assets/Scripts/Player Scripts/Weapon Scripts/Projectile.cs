using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//////////////////////////////////////////////////////
// Project: Major Project 1: Dam Buster
//Name: Andrew Fletcher
//Section: 2019FA.SGD.212.4103
//Instructor: Aisha Eskandari
// Date: 09/15/2019
//////////////////////////////////////////////////////

public class Projectile : MonoBehaviour
{
    public float speed;
    public float timeToLive;
    public float damage;
    public Transform bulletHole;

    private Vector3 destination;

    void Start()
    {
        Destroy(gameObject, timeToLive);
    }

    void Update()
    {
        if (IsDestinationReached())
        {
            Destroy(gameObject);
            return;
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (destination != Vector3.zero)
            return;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 5f))
        {
            CheckEnemy(hit);
        }
    }

    void CheckEnemy(RaycastHit hitInfo)
    {
        //var enemy = hitInfo.transform.GetComponent<EnemyHealth>();

        destination = hitInfo.point + hitInfo.normal * .0015f;

        Transform hole = (Transform)Instantiate(bulletHole, destination, Quaternion.LookRotation(hitInfo.normal) * Quaternion.Euler(0, 180, 0));
        hole.SetParent(hitInfo.transform);
        //if (enemy == null)
            //return;


        //enemy.TakeDamage(damage);
    }

    bool IsDestinationReached()
    {
        if (destination == Vector3.zero)
            return false;

        Vector3 directionToDestination = destination - transform.position;
        float dot = Vector3.Dot(directionToDestination, transform.forward);

        if (dot < 0)
            return true;

        return false;
    }
}
