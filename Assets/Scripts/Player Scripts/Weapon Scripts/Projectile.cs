using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    public float speed;
    public float timeToLive;
    public int damage;
    //public Transform bulletHole;

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

        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))
        {
            CheckEnemy(hit);
        }
    }

    void CheckEnemy(RaycastHit hitInfo)
    {
        EnemyHealth enemy = hitInfo.transform.GetComponent<EnemyHealth>();

        destination = hitInfo.point + hitInfo.normal * .0015f;

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            enemy.CheckDeath();
            print(enemy.gameObject.name + " Took damage");
        }
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
